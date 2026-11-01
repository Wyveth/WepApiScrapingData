# Performance de l'API

## Changements (branche feature/performance)

- `GET /api/v1.0/Pokemon` : chargement sans suivi EF (`GetAllReadOnly`), contenu identique.
  `GetAll` reste inchangé car il sert aux flux de synchronisation qui modifient les entités.
- Famille/variantes (`GetFamilyAndVariant`) : une requête groupée pour les évolutions au lieu de deux par Pokémon.
- Quiz et import d'attaques : `await` au lieu de `.Result` (plus de blocage de threads).
- Création d'entités : plus d'aller-retour SQL pour vérifier l'unicité de chaque GUID.
- Cache serveur (output cache, 10 min) sur les lectures du contrôleur Pokemon, hors requêtes
  authentifiées ; vidé après toute écriture réussie (hors `/graphql`).
- GraphQL : profondeur d'exécution limitée à 15.

## À vérifier avant fusion

1. `dotnet build` puis `dotnet test` (ces changements n'ont pas pu être compilés hors du poste).
2. Comparer les JSON avant/après sur `Pokemon`, `Pokemon/Light`, `Pokemon/{id}` et la route famille.
3. Mesurer les temps de réponse avant/après (même base, cache vide puis cache chaud).

## Étapes suivantes (non faites)

- Index : `Number` et `TypeEvolution` sont en `nvarchar(max)`, donc non indexables. Il faut une
  migration (longueur maximale, colonne numérique calculée pour le tri) testée sur une copie de la base.
- GraphQL : pagination (`[UsePaging]`, changement de contrat) et `RegisterDbContext`.
- Projection directe vers les DTO pour réduire le volume chargé.
