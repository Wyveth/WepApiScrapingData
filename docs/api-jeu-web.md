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
| `PokCry` (Difficile) | cri seul (`hasImage` faux, image servie seulement après la réponse) | noms de Pokémon |

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

## Duels — `api/v1.0/Duel` (JWT requis) et `/hubs/duel` (SignalR)

Migration à générer pour cette partie (table `Duels`, colonnes `Questions.ShownAt`/`AnsweredAt`, `UserProfiles.FriendCode`) :

```powershell
dotnet ef migrations add AddDuels --project WebApiScrapingData.Infrastructure --startup-project WepApiScrapingData
dotnet ef database update --project WebApiScrapingData.Infrastructure --startup-project WepApiScrapingData
```

Deux modes, au choix à chaque duel :
- `Live` (temps réel) : même question au même moment ; par défaut 15 s (Facile), 20 s (Normal) ou 25 s (Difficile) pour répondre, 5 s de compte à rebours au départ et 4 s de pause entre deux questions. Le serveur pilote le chrono (`DuelTimerService`, chaque seconde) ; la question passe dès que les deux ont répondu.
- `Async` : chacun joue le même quiz à son rythme ; celui qui lance le duel peut jouer avant qu'un adversaire ne rejoigne.

Score : 100 points par bonne réponse + jusqu'à 50 points de rapidité (linéaire sur la durée de la question).

Réglages dans `appsettings.json`, section `Duel` (pris en compte au redémarrage ; la durée par question est enregistrée sur chaque duel, donc une modification ne touche que les nouveaux duels) :

```json
"Duel": {
  "QuestionSeconds": { "Easy": 15, "Normal": 20, "Hard": 25 },
  "WinPoints": { "Easy": 1, "Normal": 2, "Hard": 3 },
  "CountdownSeconds": 5,
  "PauseSeconds": 4,
  "GraceSeconds": 1,
  "LiveSearchTimeoutMinutes": 2,
  "LiveInvitationTimeoutMinutes": 10,
  "AsyncTimeoutDays": 7
}
```

`GET /Duel/settings` renvoie les durées par difficulté au front (texte d'aide du lobby). À égalité de points, le temps total le plus court l'emporte, sinon match nul.

| Méthode | Route | Corps | Rôle |
|---|---|---|---|
| GET | `/Duel/code` | | Mon code ami (créé au premier appel), format `ABCD-EFGH` |
| POST | `/Duel/code/regenerate` | | Nouveau code ami |
| POST | `/Duel/search` | `{ "mode": "Live", "difficulty": "Normal" }` | Matchmaking : rejoint un adversaire en attente (même mode et difficulté, toutes générations) ou se met en attente |
| POST | `/Duel/challenge` | `{ "friendCode": "ABCD-EFGH", "mode": "Async", "difficulty": "Hard", "generations": [1] }` | Défi privé |
| GET | `/Duel/invitations` | | Défis reçus en attente |
| GET | `/Duel/mine` | | 30 derniers duels |
| GET | `/Duel/stats` | | Joués, victoires, défaites, nuls |
| GET | `/Duel/leaderboard` | `?period=month\|all&sort=wins\|points&top=50` | Classement public (voir ci-dessous) |
| GET | `/Duel/{id}` | | État (`DuelDto`, avec l'heure serveur pour le chrono) |
| POST | `/Duel/{id}/accept` · `/decline` | | Réponse à un défi |
| DELETE | `/Duel/{id}` | | Annule une recherche ou un défi envoyé |
| POST | `/Duel/{id}/forfeit` | | Abandon (l'adversaire gagne) |
| GET | `/Duel/{id}/questions/{order}` | `?lang=FR` | Question (mêmes règles d'affichage que le quiz solo) |
| POST | `/Duel/{id}/questions/{order}/answer` | `{ "answerIds": [..] }` | Réponse |
| GET | `/Duel/{id}/questions/{order}/image` · `/sound` | | Média du sujet (jeton requis : le front le télécharge en blob) |

Expirations : recherche temps réel 2 min, défi temps réel 10 min, recherche ou défi asynchrone 7 jours.

SignalR : connexion à `/hubs/duel?access_token=<JWT>` ; événements `duelUpdated` (Guid du duel) et `invitationsChanged`. Le client relit l'état via l'API.

Les quiz d'un duel ne sont plus accessibles via `/Play` (403) : ordre et chrono sont imposés par `/Duel`.

Limites : verrous et horloge en mémoire, prévus pour une seule instance de l'API ; pour plusieurs instances, il faudrait un backplane SignalR (Redis) et un verrou distribué.

### Classement

- Points de classement : chaque victoire rapporte `WinPoints` selon la difficulté (1 / 2 / 3 par défaut) ; défaite et match nul : 0. Un abandon compte comme une victoire pour l'adversaire.
- Points marqués : somme des scores de duel (bonnes réponses + rapidité), victoires et défaites comprises.
- `sort=wins` : points de classement, puis points marqués. `sort=points` : l'inverse. Rang partagé en cas d'égalité sur les deux.
- `period=month` : duels terminés depuis le 1er du mois (UTC) ; `period=all` : depuis le début.
- Public ; avec un JWT, la ligne du joueur est signalée (`isMe`) et renvoyée dans `me` s'il est hors du top.
- Calcul à la volée sur les duels terminés : suffisant pour quelques milliers de duels ; au-delà, prévoir une table d'agrégats.

## Pendu (`api/v1.0/Hangman`)

Deviner le nom d'un Pokémon lettre par lettre, en enchaînant les Pokémon tant qu'il reste des vies.
Solo : sans compte (la partie est identifiée par son Guid). Coop : comptes obligatoires, invitation par code ami, tour par tour, vies partagées.

| Méthode | Route | Auth | Rôle |
|---|---|---|---|
| POST | `/Hangman` | facultative | Nouvelle partie solo. Corps `{ generations: int[], lang, difficulty }` (`Easy` ou `Normal`) |
| POST | `/Hangman/coop` | oui | Invitation coop. Corps `{ friendCode, generations, lang, difficulty }` |
| GET | `/Hangman/invitations` | oui | Invitations coop reçues |
| GET | `/Hangman/mine` | oui | Parties du compte |
| GET | `/Hangman/{id}` | facultative | État de la partie |
| POST | `/Hangman/{id}/guess` | facultative | Propose une lettre `{ letter }` (A-Z, accents ignorés) |
| POST | `/Hangman/{id}/accept` | oui | Accepte l'invitation (l'invitant joue en premier) |
| POST | `/Hangman/{id}/decline` | oui | Refuse l'invitation |
| DELETE | `/Hangman/{id}` | facultative | Annule l'invitation ou abandonne (le score est gardé) |
| GET | `/Hangman/{id}/silhouette` | facultative | Image du Pokémon en cours, une fois l'indice débloqué (le client la noircit) |

Règles (section `Hangman` de appsettings.json) :
- `StartLives` 7 vies au départ, `LivesPerFound` +2 par Pokémon trouvé, plafond `MaxLives` 10.
- Une mauvaise lettre coûte une vie. À 0 vie, la partie est terminée (`Found` = score).
- Difficulté : `Easy` donne des indices, `Normal` (par défaut) aucun. Le record est calculé par mode et par difficulté.
- Indices (Easy uniquement) selon le nombre d'erreurs sur le Pokémon en cours : types (`TypeHintAt` 1), génération (`GenerationHintAt` 2), description sans le nom (`DescriptionHintAt` 4), silhouette (`SilhouetteHintAt` 6).

Réponse `HangmanDto` : `id, mode (Solo|Coop), difficulty (Easy|Normal), status (Invited|InProgress|Finished|Declined|Cancelled), lang, generations, lives, maxLives, found, mask` (`_` pour une lettre cachée, séparateurs tels quels), `letters [{letter, correct}], wrongCount, hints {types [{name,image}], generation, description, silhouette}, nextHintAt, last {name, image, found}, isMyTurn, turnUserName, partner, isInviter, bestScore, createdAt, finishedAt`.

Temps réel (coop) : hub `/hubs/duel`, événement `hangmanUpdated(id)` ; les invitations déclenchent aussi `invitationsChanged`.

## Administration (`api/v1.0/Admin`)

Réservé aux comptes listés dans `Jwt:AdminUserIds` (politique `Administrator`). `GET /Authenticate/me` renvoie `isAdmin` pour que le front affiche la page `/:loc/admin`.

| Méthode | Route | Rôle |
|---|---|---|
| GET | `/Admin/game-data` | Aperçu de la purge, sans modification |
| POST | `/Admin/game-data/purge` | Purge. Corps obligatoire `{ "confirm": "PURGE" }` (sinon 400) |

Réponse (aperçu ou résultat) : `{ quizzes, questions, answers, hangmanGames, duelQuizzesKept }`.

La purge supprime, dans une seule transaction, les quiz rattachés à aucun duel (`Quizz1Id` / `Quizz2Id`), leurs questions, leurs réponses et leurs liaisons, ainsi que toutes les parties de pendu. Les quiz des duels sont conservés : le classement ne change pas. L'opération est journalisée avec l'Id de l'administrateur.
