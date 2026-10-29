# Automatisation et préparation .NET — plan

**Objectif :** exécuter automatiquement la compilation et les tests, puis préparer une migration .NET compatible avec l'environnement de développement.

**Contexte :** les cinq projets ciblent .NET 8. Le poste dispose de Visual Studio 2022 17.14 et des SDK .NET 8/9 ; `net10.0` nécessite officiellement Visual Studio 18.0 ou ultérieur. Les modifications d'import existantes ne sont pas encore commitées et doivent être préservées.

## Contraintes

- Ne pas modifier ni se connecter à la base utilisateur ; les tests SQL utilisent un SQL Server jetable et des identifiants fictifs.
- Ne pas changer les secrets ou réécrire l'historique Git.
- Conserver la compilation dans Visual Studio 2022 tant qu'une migration de l'environnement n'a pas été décidée.
- Aucun push, fusion ou installation système sans demande explicite.

## Étapes

- [ ] Vérifier la suite de tests sous Linux, comme dans GitHub Actions, et corriger toute dépendance à Windows avec un test ciblé.
- [ ] Ajouter un workflow GitHub Actions exécutant restauration, compilation Release et tests, y compris les tests SQL sur une instance isolée et éphémère.
- [ ] Valider le workflow et les tests avec les outils disponibles ; documenter les limites des vérifications locales.
- [ ] Préparer la migration .NET 10 LTS : prérequis Visual Studio/SDK, dépendances à vérifier, tests et déploiement, sans cibler `net10.0` sur le poste actuel.
- [ ] Confirmer que les modifications d'import précédentes et les secrets utilisateur restent intacts.
