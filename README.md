# Sort It Right AR

An iPhone AR game that teaches you to sort household and market waste into the right bin.

## What it does

A lot of recyclable waste ends up in landfill because people are unsure which bin things belong in, and dangerous items like batteries, razor blades and old medicine get thrown in with everyday rubbish. Sort It Right AR turns sorting into a short, hands-on game. You place a small sorting station on a real table, items roll along a conveyor belt, and you tap each item and then the bin it belongs in. Every mistake comes with a short explanation, so you learn why an item goes where it goes.

## Features

- Place the sorting station on any flat surface with AR plane detection, then confirm or reset its position.
- Two scenarios, Household and Market, each with its own items.
- Six bins (Plastic, Paper, Organic, Residual, Hazardous, Sharps), each shown with a colour, an icon and a text label.
- Tap an item to pick it up, tap a bin to sort it. Long-press an item to inspect its name, material and a hint.
- Feedback after every sort, with extra warnings for injury risks and food that attracts animals.
- Bins fill up, take strikes for wrong items and reject a batch after three strikes.
- A live HUD with items left, accuracy, serious errors, strikes per bin and time.
- Pause at any time, and automatic pause when AR tracking is lost.
- A summary screen with your score, outcome and every mistake with its correct bin.
- History of your last 5 sessions, saved on the phone.
- A global top 10 leaderboard for each scenario, with an anonymous nickname.
- Sound effects, each with a matching visual cue, and a volume setting.

## Tech

- Unity 6.6 (6000.6.0f1) with the Universal Render Pipeline
- AR Foundation 6.6 with the Apple ARKit XR Plugin
- Unity content directories for loading and releasing each scenario's item models
- Firebase anonymous Authentication and Cloud Firestore, called through their REST APIs
- Input System and TextMesh Pro

## Requirements

- iPhone 11 or newer with iOS 16 or newer
- A Mac with Xcode and Unity 6.6 installed
- A Firebase project with Anonymous sign-in and Cloud Firestore turned on (only needed for the leaderboard)

## Setup and build

1. Clone the repository and open the folder in Unity Hub with Unity 6000.6.0f1.
2. Create `Assets/StreamingAssets/FirebaseConfig.json` with your Firebase web app details:

   ```json
   {
       "apiKey": "YOUR_WEB_API_KEY",
       "projectId": "YOUR_PROJECT_ID"
   }
   ```

   This file is ignored by git. Without it the game still works, and the leaderboard shows its offline message.
3. In Firestore, create a composite index on the `leaderboard` collection with `scenarioId` ascending and `score` descending.
4. In Unity, open `Assets/_Project/Scenes/Main.unity`, make sure the platform is iOS, and build to a folder outside `Assets`.
5. Open the generated `Unity-iPhone.xcodeproj` in Xcode, choose your team under Signing & Capabilities, plug in your iPhone and press Run.

The content directories for the scenarios are built automatically as part of the player build.

## How to play

1. Tap Start and move your phone slowly until a surface is found.
2. Tap the surface to place the station, move it if needed, then tap Confirm.
3. Choose Household or Market.
4. Tap an item on the belt, then tap the bin it belongs in. Long-press an item to inspect it.
5. Empty a bin by tapping it when it is full.
6. Sort all 30 items. Reach 80% accuracy with no serious errors to pass.
7. On the summary screen, enter a nickname to submit your score, then restart or go back to the menu.

## Project folders

Everything for the game lives in `Assets/_Project`:

- `Scenes`: the single `Main` scene. Every screen is a UI panel in it.
- `Scripts`: game code, split into `Core`, `States`, `AR`, `Simulation`, `Items`, `Bins`, `Input`, `Content`, `Data`, `UI`, `Audio` and `Editor`.
- `Data`: item and scenario data assets and the sound library.
- `Content`: the content directory roots for each scenario.
- `Prefabs`: the sorting station, bins, items, item models and UI buttons.
- `Models`, `Materials`, `Textures`, `Icons`, `Audio`, `Fonts`: art, sound and font assets.
