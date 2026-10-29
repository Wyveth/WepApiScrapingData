# Migration vers .NET 10

## Situation actuelle

- Les cinq projets et l'image Docker ciblent maintenant .NET 10.
- Le poste vérifié dispose de Visual Studio 2026 et du SDK .NET 10.
- Microsoft prend officiellement en charge la cible `net10.0` à partir de Visual Studio 18.0 (Visual Studio 2026). Un SDK .NET 10 installé seul ne rend pas la cible officiellement prise en charge dans Visual Studio 2022.
- Le support de .NET 8 prend fin le **10 novembre 2026**. .NET 10 est une version LTS prise en charge jusqu'au **14 novembre 2028**.

La cible `net10.0` n'est pas officiellement prise en charge par Visual Studio 2022. Le code est modifié localement ; aucune migration de base de données ni aucun déploiement n'ont été appliqués.

Les références Microsoft directement utilisées ont été alignées sur la version
10.0.14. Hot Chocolate a été mis à jour en 12.22.7 pour corriger
[CVE-2026-40324](https://github.com/advisories/GHSA-qr3m-xw4c-jqw3) ; le paquet
AutoMapper non utilisé a été retiré et `System.Security.Cryptography.Pkcs` est fixé
à une version corrigée. Ces corrections ne remplacent pas le renouvellement des
secrets exposés dans l'historique Git.

## Essai exploratoire

Une copie temporaire compilée dans Docker avec le SDK .NET 10 et les projets ciblant
`net10.0` a passé 65 tests ; les 8 tests SQL ont été ignorés faute d'instance de test
disponible lors de cet essai. Les paquets existants n'ont pas été mis à niveau dans
cette copie. Ce résultat aide à préparer la migration, mais ne valide ni les nouvelles
versions des dépendances ni le déploiement, et ne remplace pas les vérifications sur
une base isolée après migration.

## Vérifications avant déploiement

1. Utiliser `dotnet-ef` 10 compatible avec EF Core 10, sans écraser un outil global nécessaire à d'autres projets.
2. Exécuter tous les tests, dont les tests SQL sur un conteneur jetable, puis vérifier les routes REST, GraphQL, l'authentification, les imports et le démarrage Docker. Vérifier aussi les clients qui consomment l'API.
3. Vérifier les migrations EF et l'éventuel script SQL sur une copie de test. Planifier sauvegarde, déploiement et retour arrière avant toute intervention sur une base réelle.
4. Vérifier le workflow GitHub Actions après publication et déployer une image reconstruite avec les paramètres et secrets propres à l'environnement cible.

## Sources Microsoft

- [Compatibilité du SDK .NET et de Visual Studio](https://learn.microsoft.com/en-us/dotnet/core/porting/versioning-sdk-msbuild-vs)
- [Dates de support .NET](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)

Le renouvellement des identifiants SQL et de la clé JWT exposés dans l'historique Git reste à effectuer séparément par le propriétaire.
