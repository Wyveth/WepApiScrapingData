# Migration .NET 10 — plan d'implémentation

**Objectif :** migrer les cinq projets et leur exécution Docker/CI vers .NET 10 LTS, sans modifier la base utilisateur.

**Architecture :** conserver les routes, les données et le modèle existants ; aligner les dépendances Microsoft directement référencées sur .NET/EF Core 10, en gardant les autres bibliothèques tant qu'elles sont compatibles et sans vulnérabilité critique connue. Les migrations existantes restent inchangées.

**Référence :** `docs/migration-dotnet-10.md` et accord donné dans la conversation pour effectuer la migration après installation de Visual Studio 2026.

## Contraintes

- Préserver les changements non commités précédents ; ne pas créer de commit, pousser ou fusionner sans demande.
- Ne pas appliquer de migration ou écrire sur la base de l'utilisateur ; utiliser uniquement une instance SQL Docker jetable pour les tests.
- Ne pas renouveler ni exposer les secrets reportés par l'utilisateur.
- Conserver les contrats REST et GraphQL, les configurations et la logique d'import existants.
- Cibler `net10.0` dans les cinq projets ; utiliser les images Docker 10.0 et le SDK 10.0 en CI.
- Aligner les références Microsoft ASP.NET Core, EF Core et Extensions existantes sur la version corrective 10.0.14 ; ne pas mettre à jour à l'aveugle les bibliothèques tierces.
- Le contrôle NuGet a identifié une vulnérabilité critique dans Hot Chocolate 12.15.2 : utiliser une version corrigée de la même branche majeure et vérifier le comportement GraphQL.

## Vérifications

- Tests HTTP d'authentification et de lecture, dont la sélection de connexion Docker.
- Test GraphQL réel avec projection sur une base SQL jetable, avant et après migration.
- Tests SQL d'import, rollback et réimport sur une base jetable.
- Compilation et tests sous Windows et Linux, image Docker démarrable ; validation statique du workflow.
- Compatibilité des migrations EF vérifiée sur l'instance jetable, sans écrire sur la base réelle.

## Étapes

- [ ] Ajouter et exécuter un test GraphQL représentatif avec SQL jetable sur la version existante.
- [ ] Mettre à jour les cinq projets et les références Microsoft concernées, restaurer puis compiler ; diagnostiquer toute incompatibilité avant modification supplémentaire.
- [ ] Modifier l'image Docker, le workflow GitHub Actions et la documentation pour .NET 10.
- [ ] Lancer les tests complets sur Windows et Linux avec SQL jetable, contrôler GraphQL et vérifier le modèle/migrations EF sans toucher à la base utilisateur.
- [ ] Revoir les changements ; signaler les limites et les actions manuelles restantes.
