#!/bin/bash
# Préparation de la base, lancée une fois à chaque démarrage par le service db-init (sans effet si tout est déjà en place) :
#  1. si la base DB_NAME n'existe pas : restaure backup/DB_BACKUP_FILE s'il est présent, sinon crée une base vide
#     (le schéma est alors créé par les migrations EF au démarrage de l'API) ;
#  2. crée le compte SQL de l'API (APP_DB_USER) s'il n'existe pas, et le rend propriétaire de la base (db_owner).
set -euo pipefail

SQLCMD=(/opt/mssql-tools18/bin/sqlcmd -C -S db -U sa -P "$SA_PASSWORD" -b)
BACKUP="/var/opt/mssql/backup/${DB_BACKUP_FILE}"

# Noms passés dans du T-SQL : on refuse tout caractère inattendu
for value in "$DB_NAME" "$APP_DB_USER"; do
  if [[ ! "$value" =~ ^[A-Za-z0-9_]+$ ]]; then
    echo "Nom invalide : '$value' (lettres, chiffres et _ uniquement)" >&2
    exit 1
  fi
done
APP_PWD_SQL=${APP_DB_PASSWORD//\'/\'\'}

echo "Attente de SQL Server..."
for i in $(seq 1 60); do
  "${SQLCMD[@]}" -Q "SELECT 1" -o /dev/null && break
  sleep 3
done

exists=$("${SQLCMD[@]}" -h -1 -W -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE name = N'$DB_NAME'" | tr -d '[:space:]')

if [[ "$exists" == "0" ]]; then
  if [[ -f "$BACKUP" ]]; then
    echo "Restauration de $DB_BACKUP_FILE dans la base $DB_NAME..."
    # Fichiers logiques de la sauvegarde (données et journal), déplacés dans le dossier de données du conteneur
    moves=""
    while IFS='|' read -r logical physical type _; do
      logical=$(echo "$logical" | xargs); type=$(echo "$type" | xargs)
      [[ -z "$logical" ]] && continue
      if [[ "$type" == "L" ]]; then ext="ldf"; else ext="mdf"; fi
      [[ "$type" == "D" || "$type" == "L" ]] || continue
      moves+=", MOVE N'${logical}' TO N'/var/opt/mssql/data/${DB_NAME}_${logical}.${ext}'"
    done < <("${SQLCMD[@]}" -h -1 -W -s '|' -Q "SET NOCOUNT ON; RESTORE FILELISTONLY FROM DISK = N'$BACKUP'" | cut -d'|' -f1-3)

    "${SQLCMD[@]}" -Q "RESTORE DATABASE [$DB_NAME] FROM DISK = N'$BACKUP' WITH RECOVERY, STATS = 10${moves}"
    echo "Restauration terminée."
  else
    echo "Aucune sauvegarde ($BACKUP) : création d'une base vide $DB_NAME (schéma créé par les migrations)."
    "${SQLCMD[@]}" -Q "CREATE DATABASE [$DB_NAME]"
  fi
else
  echo "La base $DB_NAME existe déjà : pas de restauration."
fi

echo "Compte applicatif $APP_DB_USER..."
"${SQLCMD[@]}" -Q "
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = N'$APP_DB_USER')
    CREATE LOGIN [$APP_DB_USER] WITH PASSWORD = N'$APP_PWD_SQL', CHECK_POLICY = ON;
ELSE
    ALTER LOGIN [$APP_DB_USER] WITH PASSWORD = N'$APP_PWD_SQL';
"
# Un utilisateur restauré avec la sauvegarde peut porter le même nom sans être relié au login : on le recrée proprement
"${SQLCMD[@]}" -d "$DB_NAME" -Q "
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'$APP_DB_USER')
    ALTER USER [$APP_DB_USER] WITH LOGIN = [$APP_DB_USER];
ELSE
    CREATE USER [$APP_DB_USER] FOR LOGIN [$APP_DB_USER];
ALTER ROLE db_owner ADD MEMBER [$APP_DB_USER];
"
echo "Base prête."
