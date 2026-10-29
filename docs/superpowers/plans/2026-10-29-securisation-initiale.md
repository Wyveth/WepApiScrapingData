# Sécurisation initiale — plan d'implémentation

**Objectif :** réduire les risques immédiats avant de modifier la logique métier.

**Architecture :** conserver l'API et Identity ; protéger les opérations d'administration par une politique qui accepte uniquement les identifiants utilisateurs explicitement configurés côté serveur. Les paramètres sensibles sont fournis hors du dépôt. Les changements plus larges sur l'import et les performances seront traités séparément.

**Technologies :** ASP.NET Core 8, Entity Framework Core, JWT, NUnit, SQL Server.

**Référence :** état des lieux et priorités présentés dans la conversation ; aucune spécification distincte n'a été créée.

## Contraintes

- Partir de `origin/master`, sur `feature/securisation-fiabilisation`.
- Ne pas modifier la base de données, faire de migration, réécrire l'historique, publier ou changer les mots de passe réels.
- Garder les lectures publiques et les routes d'authentification nécessaires ; un compte ordinaire ne doit pas obtenir de droits d'administration en s'inscrivant.
- Configuration administrateur absente ou vide : accès administratif refusé.
- Ne pas utiliser des identifiants ou clés réels dans les tests ou la documentation.
- La suppression des secrets du dépôt ne révoque pas les secrets déjà exposés : leur rotation reste une opération externe à organiser.

## Points à vérifier

- Un utilisateur non connecté reçoit 401 sur une route administrative.
- Un utilisateur authentifié non administrateur reçoit 403, même s'il vient de s'inscrire.
- Un utilisateur explicitement configuré administrateur peut franchir la politique, sans accès à une base réelle dans les tests.
- Une configuration utilisateur absente ne donne aucun droit administratif.
- Les lectures publiques et les routes `register`, `login` et `refresh` restent accessibles sans authentification.

## Tâche 1 : Configuration et secrets

**Fichiers :** `WepApiScrapingData/appsettings.json`, `WepApiScrapingData/appsettings.Development.json`, `WebApiScrapingData.Infrastructure/Settings/appSettings.json`, `WebApiScrapingData.Infrastructure/Data/ScrapingContextFactory.cs`, `WepApiScrapingData/Program.cs`, `README.md`, tests de configuration.

- [ ] Ajouter des tests de chargement de configuration vérifiant que les variables d'environnement et les secrets de développement peuvent remplacer les valeurs de fichiers, et que la factory EF utilise la configuration externe sans préférence pour un secret embarqué.
- [ ] Exécuter les tests et constater les échecs liés à l'ordre de chargement actuel.
- [ ] Supprimer les mots de passe et clés JWT des trois fichiers versionnés ; conserver des exemples non sensibles et utiliser les fournisseurs de configuration standards. Corriger la priorité des variables d'environnement pour la factory EF.
- [ ] Documenter les paramètres `ConnectionStrings__PokemonDataBase`, `Jwt__Key`, et la rotation nécessaire ; préciser que le changement de clé invalide les JWT existants.
- [ ] Exécuter les tests ciblés, puis la suite ; consigner les échecs réseau préexistants.

## Tâche 2 : Autorisation des opérations sensibles

**Fichiers :** `WepApiScrapingData/ExtensionMethods/SecurityMethods.cs`, `WebApiScrapingData.Infrastructure/Configurations/SecurityOption.cs`, contrôleurs d'administration et `Controllers/Abstract/GenericController.cs`, tests HTTP, `README.md`.

**Interface :** politique `Administrator` ; liste d'identifiants Identity sous `Jwt:AdminUserIds`. Comparer les identifiants de l'utilisateur authentifié à cette liste, sans s'appuyer sur un nom ou un rôle fourni par le client.

- [ ] Ajouter des tests HTTP couvrant les cinq points de vérification ci-dessus, à l'aide d'une clé JWT de test et sans connexion SQL réelle. Inclure les actions héritées d'écriture et au moins une route d'administration déclenchée par GET.
- [ ] Exécuter les tests et constater que les routes sensibles sont actuellement accessibles sans autorisation.
- [ ] Ajouter la politique et protéger les actions d'écriture du contrôleur générique ainsi que les contrôleurs d'import, migration, synchronisation, scraping, export et diagnostic. Vérifier séparément les actions de quiz qui écrivent malgré leur verbe GET.
- [ ] Documenter la configuration des identifiants administrateurs et l'impact sur les clients existants.
- [ ] Exécuter les tests ciblés et la suite complète ; vérifier explicitement les routes publiques et les refus 401/403.

## Tâche 3 : Déploiement et traçabilité

**Fichiers :** `WepApiScrapingData/Dockerfile`, `WepApiScrapingData/Program.cs`, `README.md`, tests de configuration.

- [ ] Ajouter une vérification du comportement hors développement : pas de page d'exception détaillée ; configuration HTTPS documentée pour un déploiement derrière proxy.
- [ ] Exécuter la vérification avant modification et constater le problème lié à l'environnement Docker.
- [ ] Retirer le forçage de l'environnement Development de l'image Docker. Documenter les responsabilités TLS du proxy et de l'application sans supposer la topologie de déploiement.
- [ ] Recompiler, relancer les tests et examiner le diff. Signaler les risques et les étapes manuelles restantes.

## Hors périmètre de cette première tranche

Stabilisation des tests de scraping externes, refonte des imports, optimisation EF, mise à niveau .NET et modification du modèle de quiz : ces sujets restent à planifier séparément après sécurisation.
