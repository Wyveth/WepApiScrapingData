# WepApiScrapingData
Api ScrapData for Get All Data Pokemon For App Xamarin

Generate Json with Pokemon Info in French, English, Spanish, Italian & German.

Datascraping with Recursive Fonction for get all data on website api.pokemon.com

## Configuration et lancement

L'API cible .NET 8 et utilise SQL Server. Les fichiers versionnés ne contiennent
pas d'identifiants utilisables : il faut fournir la configuration avant le lancement.

- `ConnectionStrings__PokemonDataBase` : chaîne de connexion SQL Server.
- `Jwt__Key` : clé secrète JWT propre à l'environnement. Pour HS512, utiliser
  au moins 64 octets aléatoires et conserver cette clé dans un gestionnaire de secrets.
- `Jwt__AdminUserIds__0`, `Jwt__AdminUserIds__1`, etc. : identifiants Identity
  des comptes autorisés à administrer les données. Sans liste, aucun utilisateur
  n'a accès à ces opérations.

En développement, les secrets utilisateur .NET sont également pris en charge :

```powershell
dotnet user-secrets set "ConnectionStrings:PokemonDataBase" "<chaine-de-connexion>" --project WepApiScrapingData
dotnet user-secrets set "Jwt:Key" "<cle-aleatoire-secrete>" --project WepApiScrapingData
dotnet user-secrets set "Jwt:AdminUserIds:0" "<identifiant-identity>" --project WepApiScrapingData
dotnet run --project WepApiScrapingData
```

Utilisez vos propres valeurs et évitez d'exposer les secrets dans l'historique du
terminal ou les journaux. L'identifiant Identity du compte connecté est renvoyé
par `GET /api/v1.0/Authenticate/me`. La liste administrateur est lue au démarrage :
redémarrez l'application après modification. La configuration des outils EF peut
utiliser `ConnectionStrings__PokemonDataBase` ou, pour compatibilité, `DB_CONNECTION`.

Les opérations d'écriture génériques, d'import, de migration, de synchronisation,
de scraping, d'export et de diagnostic nécessitent désormais un JWT associé à un
identifiant administrateur configuré. Certaines routes de quiz sont également
restreintes car elles modifient des données. Les clients existants qui les
appellent doivent être adaptés ; les lectures et l'authentification restent publiques.

**Important :** des identifiants SQL et une clé JWT ont été présents dans l'historique
Git. Les retirer des fichiers actuels ne les révoque pas. Renouvelez le mot de passe
SQL et la clé JWT auprès de vos systèmes, puis mettez à jour les configurations de
déploiement. Changer la clé JWT invalide les jetons d'accès existants. Examinez aussi
les copies et l'historique du dépôt selon votre politique de sécurité.

## Docker et HTTPS

Construire l'image depuis la racine :

```powershell
docker build -f WepApiScrapingData/Dockerfile -t pokemon-api .
```

Pour le débogage Docker avec Visual Studio et un SQL Server accessible sur le poste
Windows, configurez le secret utilisateur `ConnectionStrings:PokemonDataBaseDocker`
avec la même base et les mêmes identifiants que `ConnectionStrings:PokemonDataBase`,
mais avec `Server=host.docker.internal,1433`. Dans le conteneur, `127.0.0.1`
désigne le conteneur lui-même. Visual Studio doit monter les secrets utilisateur
dans le conteneur ; redémarrez l'application après les avoir modifiés.

Cette connexion spécifique est utilisée uniquement en environnement Development
dans un conteneur, lorsqu'elle est renseignée ; sinon, l'application utilise
`ConnectionStrings:PokemonDataBase`. En production, configurez directement
`ConnectionStrings__PokemonDataBase` pour le réseau réel du déploiement.

L'image n'impose plus l'environnement Development : ASP.NET Core utilise Production
par défaut. Fournissez les paramètres ci-dessus par un mécanisme de secrets adapté
à votre infrastructure, et non dans l'image.

L'image écoute en HTTP sur le port 80. Elle ne configure pas à elle seule une
terminaison TLS ni une redirection HTTP vers HTTPS. En production, placez-la derrière
un proxy qui termine HTTPS et empêche l'accès HTTP direct non fiable, ou configurez
explicitement HTTPS et la gestion du proxy dans votre infrastructure.

## Tests

```powershell
dotnet test WepApiScrapingData.sln
```

Les tests de scraping utilisent des réponses HTTP locales et ne dépendent pas de la
disponibilité des sites externes. Ils ne garantissent pas que le format actuel de ces
sites est toujours compatible. Les tests d'autorisation utilisent une clé et une
connexion fictives, sans base de données réelle.

## Imports de fichiers

Les routes `SaveInDB`, `SaveGenInDB/{gen}` et `SaveInfoPokemonAttackInDB` lisent les
fichiers du répertoire `PokeScrap` sous la racine de l'application et attendent la fin
du traitement avant de répondre. Leurs règles d'import restent celles des repositories
existants : elles ne garantissent ni transaction globale ni reprise sans doublon. Les
autres imports n'ont pas encore été refactorisés. Sauvegardez la base et vérifiez les
fichiers avant tout import ; les tests automatisés n'importent rien dans la base réelle.
