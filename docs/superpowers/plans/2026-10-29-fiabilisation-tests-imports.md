# Fiabilisation des tests et des imports — plan d'implémentation

**Objectif :** rendre les tests de scraping reproductibles et sécuriser un premier flux d'import sans toucher aux données existantes.

**Architecture :** des pages et réponses HTTP locales remplacent les accès Internet dans la suite courante. Les imports de fichiers Pokémon sont coordonnés par un service asynchrone distinct du contrôleur ; les routes et les formats existants restent inchangés.

**Technologies :** .NET 8, NUnit, ASP.NET Core, EF Core, SQL Server.

**Référence :** demande de poursuivre les points 2 et 3 de l'état des lieux. Le renouvellement des secrets reste à la charge de l'utilisateur et n'est pas effectué ici.

## Contraintes

- Conserver les changements non commités de la première tranche et travailler sur la branche existante.
- Ne pas exécuter d'import, de migration ou de modification sur la base de l'utilisateur.
- Conserver les trois routes `SaveInDB`, `SaveInfoPokemonAttackInDB` et `SaveGenInDB/{gen}` et leurs fichiers d'entrée.
- Ne pas masquer les tests réseau en les supprimant ou en les ignorant : conserver leur intention avec des fixtures contrôlées.
- Ne pas prétendre avoir rendu tous les imports transactionnels ou idempotents : les autres flux restent à traiter.

## Points à vérifier

- Une réponse HTTP en erreur ne doit pas être interprétée comme du HTML valide.
- Les traductions sont extraites depuis une page locale représentative, sans dépendance à un site tiers.
- Un import doit rester en cours tant que son traitement asynchrone n'est pas terminé.
- Une erreur d'import doit remonter à l'appelant ; un fichier absent ne doit pas déclencher d'écriture.
- Le bon fichier doit être utilisé pour chaque route, y compris selon la génération.

## Tâche 1 : Tests de scraping indépendants du réseau

**Fichiers :** `WebApiScrapingData.Test/Utils/HttpClientUtilsTest.cs`, `WebApiScrapingData.Test/Utils/TranslationUtilsTest.cs`, nouveau serveur HTTP local de test, `WepApiScrapingData/Utils/HttpClientUtils.cs`.

- [ ] Ajouter un test HTTP local qui exige une exception sur un statut 403 ; vérifier son échec avant correction.
- [ ] Faire vérifier le statut HTTP dans `CallUrl` avant de lire le corps et réutiliser un client HTTP ; vérifier le test.
- [ ] Remplacer les appels Internet des trois tests historiques par un serveur local et une page HTML de traduction représentative ; conserver les assertions de traduction et vérifier la suite.

## Tâche 2 : Premier flux d'import maintenable

**Fichiers :** `WebApiScrapingData.Framework/Repositories/IRepositoryExtendsPokemon.cs`, `WebApiScrapingData.Infrastructure/Repository/Class/PokemonRepository.cs`, `WepApiScrapingData/Services/PokemonFileImportService.cs`, `WepApiScrapingData/Controllers/DatabaseController.cs`, `WepApiScrapingData/ExtensionMethods/DIMethods.cs`, tests du service.

- [ ] Créer une interface limitée aux deux opérations d'import et des tests avec des fichiers temporaires et un importeur contrôlé : fichier exact, attente de fin, remontée d'erreur, absence de fichier.
- [ ] Vérifier que les tests échouent avant d'introduire le service.
- [ ] Implémenter le service asynchrone ; injecter l'interface et déléguer les trois routes. Attendre les tâches au lieu de les lancer sans attente, sans modifier la logique métier des repositories.
- [ ] Exécuter les tests du service et les tests de sécurité des routes, sans base réelle.

## Tâche 3 : Documentation et vérification

- [ ] Documenter les tests locaux, la portée limitée du refactoring et le risque restant des imports non transactionnels.
- [ ] Exécuter `dotnet test WepApiScrapingData.sln --no-restore` et examiner les résultats et le diff.
- [ ] Rappeler que les identifiants exposés doivent toujours être renouvelés ultérieurement par l'utilisateur.
