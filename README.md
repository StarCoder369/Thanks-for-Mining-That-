# Thanks For Mining That

A game where you let the enemies mine resources for you!

<img width="515" height="289" alt="Screenshot 2026-07-26 at 1 17 21 PM" src="https://github.com/user-attachments/assets/88bd971d-eac4-47ca-a6ef-6be302c99486" />

---

## Demo

**Link:** [Thanks For Mining That](https://starcoder.itch.io/thanks-for-mining-that)

**GitHub:** [GitHub Page](https://github.com/StarCoder369/Thanks-for-Mining-That-)

The itch.io page contains a web build which you can play without downloading any files.

## How To Play

**WASD/Mouse** - Movement
You can choose anyway you want. Recommended to use mouse.

**E** - Open Crafting Panel

**LMB/Space** - Deploy tool

​**1, 2, 3, 4​** - Switch Tools

**Instructions:**
The point of the game is to unlock and use the **Black Hole**, which is the final tool, as fast as possible. It requires all other tools unlocked and a large amount of coins. It also requires a large amount of resources to craft in-game.

When you click play for the first time, you start with the Gravity Orb tool. Resources are inside asteroids, so you must lure enemies close and make them crash into the asteroids to get resources. Destroying asteroids and enemies also gives you coins which can be used to unlock tools.

**Game Flow:**

1. Click play
2. Try to survive as long as possible
3. Run out of health
4. Unlock and equip more tools
5. Play and repeat 1-5 until black hole is unlocked
6. After black hole is unlocked, click play
7. Craft and use black hole after getting enough resources
8. The stats screen will appear showing you your stats
9. Click new run, and repeat steps 1-8 to try to get the best stats(Like the fastest, least deaths, most enemies destroyed, etc.)

## Run Locally

It is recommended to play the web version in itch.io, but if you really want to run it locally, this is how to do it.

There are multiple ways to run the project locally. The first way is recommended if you only want to play or explore the project. The other methods are better if you want to receive updates(highly likely there will be no updates) or contribute.

> **You must have Unity Hub and Unity installed before continuing.**

### Check the Unity Version

Before opening the project:

1. Open the `ProjectSettings/ProjectVersion.txt` file in the repository, or check the Unity version shown on the GitHub page.
2. Make sure you have that exact Unity version (or the recommended LTS version) installed in Unity Hub.
3. If you don't, install it through Unity Hub before opening the project.

> **Warning:** Opening the project with a different Unity version may cause compatibility issues.

---

## First Way (Download ZIP)

1. Go to the [GitHub Page](https://github.com/StarCoder369/Thanks-for-Mining-That-).
2. Click **Code** → **Download ZIP**.
3. Extract the ZIP to a folder on your computer.
4. Open **Unity Hub**.
5. Click **Add** → **Add Project From Disk**.
6. Select the extracted project folder (the folder containing `Assets`, `Packages`, and `ProjectSettings`).
7. Open the project in Unity.

---

## Second Way (Clone with Git)

1. Install Git if you don't already have it.
2. Open a terminal or command prompt.
3. Clone the repository:
   ```
   git clone https://github.com/StarCoder369/Thanks-for-Mining-That-.git
   ```
4. Open **Unity Hub**.
5. Click **Add** → **Add Project From Disk**.
6. Select the cloned project folder.
7. Open the project in Unity.

---

## Second Way (Add from Git Repository)

1. Open **Unity Hub**.
2. Click **Add** → **Add Project From Git Repository**.
3. Enter the repository URL:
   ```
   https://github.com/StarCoder369/Thanks-for-Mining-That-.git
   ```
4. Choose where you want the project to be saved.
5. Wait for Unity Hub to clone the repository.
6. Open the project in Unity.

> **Warning:** "Add Project From Git Repository" is only available in newer versions of Unity Hub and may not be supported on all installations. If you don't see the option, use the **Clone with Git** method instead.

## Tools

**Gravity Orb:** Creates a temporary gravity field, pulling enemies and asteroids to it.

**Pulse Wave:** Emits a wave that disrupts enemy formations.

**Repelling Orb:** Creates a temporary repelling field, pushing enemies and asteroids away from it.

**Asteroid Lock:** Shoots out a lock, that upon collision with an asteroid, locks it in place for some time.

**Grow Tool:** Upon collision with an asteroid, makes it grow. (Stackable)

**Asteroid Forge:** Creates a new asteroid in a short amount of time.

**Ship Decoy:** Creates a decoy that lures enemies away.

**Black Hole:** Shoots out a black hole that rapidly grows, consuming enemies and asteroid in the way. When it grows too big, it consumes the entire universe. It then leads to the Stats Screen, where the player can see current run and all run stats. This is the way to 'win' the game.

## Enemies

There are two kinds of enemies. One is slow, but turns faster, while the other is fast, and turns slower.

Below you can see the two kinds of enemies. The first one is the fast, while the second is the slow.

![Fast Enemy](EnemySpaceship1.png)
![Slow Enemy](EnemySpaceship.png)
