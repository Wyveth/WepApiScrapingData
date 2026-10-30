# API du jeu web (quiz, favoris, profil)

Branche `feature/quiz-web`. Endpoints destinés au front Angular PokeQuizz-Dex (puis à l'app mobile).

## Mise en place

1. Créer la migration des nouvelles tables `Favorites` et `UserProfiles` :

   ```powershell
   dotnet ef migrations add AddFavoritesAndUserProfiles --project WebApiScrapingData.Infrastructure --startup-project WepApiScrapingData
   dotnet ef database update --project WebApiScrapingData.Infrastructure --startup-project WepApiScrapingData
   ```

2. Les tables `Difficulties` et `QuestionTypes` sont alimentées automatiquement à la première création de quiz
   (catalogue porté de l'app Xamarin, `Services/Game/QuizzCatalog.cs`). L'opération est idempotente.

## Quiz — `api/v1.0/Play`

Ouvert aux invités. Avec un JWT, le quiz est rattaché au compte et n'est visible que par lui.
Un quiz invité n'est accessible qu'avec son `id` (Guid), à conserver côté navigateur.

| Méthode | Route | Corps / paramètres | Réponse |
|---|---|---|---|
| POST | `/Play` | `{ "generations": [1,2], "difficulties": ["Easy"] }` (0 = Hisui ; vide = toutes / Easy) | 201 `QuizzSummaryDto` |
| GET | `/Play/mine` | JWT requis | `QuizzSummaryDto[]` (50 derniers) |
| GET | `/Play/{id}` | | `QuizzSummaryDto` |
| GET | `/Play/{id}/questions/{order}` | `?lang=FR` | `QuestionDto` (sans correction tant que non répondue) |
| POST | `/Play/{id}/questions/{order}/answer` | `{ "answerIds": [123] }` | `AnswerResultDto` |
| GET | `/Play/{id}/questions/{order}/image` | | image du sujet (nom de fichier non exposé) |
| GET | `/Play/{id}/questions/{order}/sound` | | cri du Pokémon sujet |
| GET | `/Play/{id}/result` | `?lang=FR` | `QuizzResultDto` |
| POST | `/Play/{id}/claim` | JWT requis | rattache un quiz invité au compte |
| DELETE | `/Play/{id}` | | 204 |

Règles :
- 10 / 15 / 20 questions selon la difficulté la plus facile cochée (comme dans l'app Xamarin).
- Correction côté serveur : `isCorrect` uniquement si toutes les bonnes réponses et aucune mauvaise sont cochées.
- Une question déjà répondue n'est pas modifiable (`alreadyAnswered: true`).
- `isBlurred`, `isGrayscale`, `isHide` : effets à appliquer en CSS sur l'image du sujet.
- Les descriptions de Pokémon ont le nom de la famille d'évolution masqué par `[...]`.
- Erreurs fonctionnelles : `{ "message": "..." }` avec 400 ou 404.

### Types de sujet (`subject.kind`)

| Code question | Sujet affiché | Réponses |
|---|---|---|
| `Pok` | image (nom masqué) | noms de Pokémon |
| `PokDesc` | Pokémon | descriptions |
| `PokDescReverse` | description | noms de Pokémon |
| `TypPok`, `TypPokVarious`, `TypWeakPokVarious` | Pokémon | types (1 ou plusieurs) |
| `Typ` | icône du type | noms de types |
| `PokTypVarious` | type | Pokémon (plusieurs) |
| `PokFamilyVarious` | Pokémon | Pokémon de la famille (plusieurs) |
| `TypPokTalentVarious` | Pokémon | talents (plusieurs) |
| `Talent` | nom du talent | descriptions |
| `TalentReverse` | description du talent | noms de talents |
| `PokStat` | Pokémon (dans le libellé) | valeurs numériques |

## Favoris et profil — `api/v1.0/Me` (JWT requis)

| Méthode | Route | Corps | Réponse |
|---|---|---|---|
| GET | `/Me/favorites/ids` | | `long[]` |
| GET | `/Me/favorites?lang=FR` | | `PokemonLightDto[]` |
| PUT | `/Me/favorites/{pokemonId}` | | 204 (idempotent) |
| DELETE | `/Me/favorites/{pokemonId}` | | 204 (idempotent) |
| POST | `/Me/favorites/import` | `{ "pokemonIds": [1,4,7] }` | `long[]` (fusion des favoris invité) |
| GET | `/Me/profile?lang=FR` | | `ProfileDto` |
| PUT | `/Me/profile` | `{ "avatarPokemonId": 25 }` | `ProfileDto` |

## Points ouverts

- Pas de limitation de débit sur `POST /Play` en invité (chaque quiz écrit ~200 lignes) : à ajouter avant mise en ligne publique.
- Le flou/masquage est appliqué côté client : l'image d'origine reste téléchargeable via `/image`.
- L'ancienne génération (`QuizzController.GenerateQuizz`, `AnswerRepository`) reste en place, réservée aux admins ; elle n'est plus utilisée par le jeu web.
