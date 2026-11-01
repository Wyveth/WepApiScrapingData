# Déploiement PokeQuizz (Docker)

Un seul serveur Linux fait tourner les cinq services :

| Service | Rôle |
|---|---|
| `db` | SQL Server, données dans le volume `mssql-data` (non exposé sur Internet) |
| `db-init` | Restaure la sauvegarde au premier démarrage et crée le compte SQL de l'API, puis s'arrête |
| `api` | API .NET 10 ; applique les migrations EF au démarrage |
| `front` | Front Angular servi par nginx |
| `caddy` | Reverse proxy sur 80/443, certificat HTTPS Let's Encrypt automatique |

Tout passe par un seul domaine. `https://DOMAIN/` sert le front ; `/api`, `/hubs` (temps réel des duels) et `/Content` (images, cris) vont à l'API.

## 1. Prérequis

- Un VPS Linux 64 bits (x86_64), avec au moins 2 Go de RAM pour SQL Server (4 Go conseillés).
- Docker et le plugin Docker Compose installés.
- Un nom de domaine dont l'enregistrement DNS A (et AAAA si IPv6) pointe vers l'IP du serveur.
- Les ports 80 et 443 ouverts dans le pare-feu.

## 2. Sur votre poste : préparer la base et les médias

1. Créez d'abord la migration `AddHangman` si ce n'est pas fait : l'API applique les migrations au démarrage, encore faut-il qu'elles existent dans le code.
2. Vérifiez la version de votre SQL Server de dev avec `SELECT @@VERSION;`. L'image `MSSQL_IMAGE` du `.env` doit être de la même version majeure ou d'une version plus récente.
3. Faites une sauvegarde complète de la base de dev, en lecture seule pour la base :
   ```sql
   BACKUP DATABASE [NomDeVotreBase]
   TO DISK = N'C:\Temp\pokequizz.bak'
   WITH COPY_ONLY, COMPRESSION, INIT, STATS = 10;
   ```
   `COPY_ONLY` évite de perturber la chaîne de sauvegardes existante. Si votre édition ne gère pas `COMPRESSION` (Express), retirez cette option.
4. Le dossier `WepApiScrapingData/Content` (images et cris, environ 1 Go) n'est pas dans Git : il sera copié à part.

## 3. Sur le serveur : installer

```bash
sudo mkdir -p /opt/pokequizz && sudo chown $USER /opt/pokequizz && cd /opt/pokequizz
git clone <url-du-depot-back>  WebApiScrapingData
git clone <url-du-depot-front> PokeQuizz-Dex
mkdir -p content WebApiScrapingData/deploy/backup
```

Depuis votre poste, copiez la sauvegarde et les médias (PowerShell, OpenSSH inclus dans Windows) :

```powershell
scp C:\Temp\pokequizz.bak utilisateur@serveur:/opt/pokequizz/WebApiScrapingData/deploy/backup/
scp -r "D:\C# Project\WebApiScrapingData\WepApiScrapingData\Content\*" utilisateur@serveur:/opt/pokequizz/content/
```

Le fichier de sauvegarde doit être lisible par SQL Server dans le conteneur :

```bash
chmod 644 /opt/pokequizz/WebApiScrapingData/deploy/backup/*.bak
```

## 4. Configurer

```bash
cd /opt/pokequizz/WebApiScrapingData/deploy
cp .env.example .env
nano .env
```

Remplissez au minimum les variables suivantes : `DOMAIN`, `ACME_EMAIL`, `SA_PASSWORD`, `DB_NAME` (le nom de votre base de dev), `APP_DB_PASSWORD` et `JWT_KEY`.

Pour générer des secrets :

```bash
openssl rand -base64 64 | tr -d '\n'   # JWT_KEY
openssl rand -base64 24                 # mots de passe (ajoutez un symbole si besoin)
```

Pour vous donner l'accès administrateur, mettez l'`Id` de votre compte (table `AspNetUsers`) dans `ADMIN_USER_ID`.

## 5. Lancer

```bash
docker compose up -d --build
docker compose logs -f db-init api    # restauration, puis migrations au démarrage de l'API
```

Le premier démarrage prend quelques minutes, le temps de construire les images et de restaurer la base. Le site est ensuite disponible sur `https://DOMAIN`.

Une fois la restauration réussie, vous pouvez supprimer le `.bak` du serveur : la base vit maintenant dans le volume `mssql-data`, et `db-init` ne restaure jamais une base qui existe déjà.

## 6. Mettre à jour

```bash
cd /opt/pokequizz
git -C WebApiScrapingData pull
git -C PokeQuizz-Dex pull
cd WebApiScrapingData/deploy
docker compose up -d --build
```

Les nouvelles migrations EF sont appliquées au redémarrage de l'API.

## 7. Exploitation

**État et journaux**

```bash
docker compose ps
docker compose logs -f api
```

**Sauvegarder la base du serveur** (le fichier est écrit dans le volume de données, puis copié sur l'hôte) :

```bash
docker compose exec db bash -c "/opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -P \"\$MSSQL_SA_PASSWORD\" \
  -Q \"BACKUP DATABASE [NomDeVotreBase] TO DISK = N'/var/opt/mssql/data/sauvegarde.bak' WITH INIT, STATS = 10\""
docker compose cp db:/var/opt/mssql/data/sauvegarde.bak ./sauvegarde-$(date +%F).bak
```

**Accéder à la base depuis votre poste** (SSMS ou Azure Data Studio), sans ouvrir de port sur Internet :

1. Exposez temporairement le port en local seulement, en ajoutant `ports: ["127.0.0.1:1433:1433"]` au service `db`, puis lancez `docker compose up -d`.
2. Ouvrez le tunnel depuis votre poste : `ssh -L 1433:127.0.0.1:1433 utilisateur@serveur`.
3. Connectez-vous à `localhost,1433`.

**Repartir de zéro sur la base** (supprime toutes les données du serveur) : `docker compose down -v`, puis relancez avec un `.bak`.
