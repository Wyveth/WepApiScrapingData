# Import Pokémon par génération — plan d'implémentation

**Objectif :** rendre les imports JSON Pokémon relançables et éviter les écritures partielles, sans modifier la base existante pendant les tests.

**Architecture :** `PokemonRepository.SaveJsonInDb` vérifie les entrées, identifie chaque forme par son numéro normalisé, son type d'évolution et son nom français complet, puis conserve les enregistrements existants. Une transaction SQL Server englobe les écritures, y compris les informations de langue. `SaveInDB` partage ce même repository et bénéficie donc aussi du changement.

**Technologies :** .NET 8, EF Core 8, SQL Server, NUnit.

**Référence :** décision prise dans la conversation : ne pas écraser les données existantes ; ajouter uniquement les Pokémon et formes manquants.

## Contraintes

- Aucun import ni migration sur la base de l'utilisateur ; utiliser exclusivement un conteneur SQL Server jetable pour les tests d'intégration.
- Ne pas modifier le schéma ni les routes publiques dans cette tranche.
- Les identifiants exposés restent à renouveler ultérieurement par l'utilisateur.
- Ne pas modifier les entrées existantes, même si les champs importés ont changé.
- Le numéro, le type d'évolution et le nom français complet doivent tous être renseignés ; comparer sans tenir compte de la casse et des espaces en bordure, et normaliser les zéros en tête du numéro.
- Les autres flux d'import ne sont pas concernés et ne deviennent pas automatiquement transactionnels ou sans doublons.

## Vérifications

- Le même fichier importé deux fois ne crée pas de nouveaux Pokémon ou données de langue et ne modifie pas les données existantes.
- Deux formes partageant le numéro et le type d'évolution mais ayant des noms complets distincts sont conservées.
- Les numéros `006` et `6` représentent le même numéro.
- Une entrée invalide ne doit produire aucune écriture ; une erreur après une écriture doit annuler tout l'import, langues comprises.
- Deux imports simultanés ne doivent pas créer de doublons ; une transaction peut être refusée en cas d'interblocage et doit alors être relancée par son appelant.

## Étapes

- [ ] Ajouter des tests d'intégration SQL Server ciblant seulement une instance explicitement dédiée aux tests. Couvrir les cinq vérifications ci-dessus, avec des Pokémon synthétiques ; observer les échecs avant correction.
- [ ] Dans `WebApiScrapingData.Infrastructure/Repository/Class/PokemonRepository.cs`, valider le JSON et les identités avant les écritures, exécuter l'import dans une transaction sérialisable et ignorer les identités déjà vues en base ou dans le fichier.
- [ ] Exécuter les tests d'intégration contre un SQL Server Docker jetable puis la suite normale ; aucun test ne doit se connecter à la base de l'utilisateur.
- [ ] Mettre à jour `README.md` avec les garanties et limites de ce flux, ainsi que l'exécution explicite des tests SQL isolés.
- [ ] Relire les changements et vérifier qu'aucune donnée ni secret réel n'est ajouté au dépôt.
