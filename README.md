# VR Training

A multi-puzzle VR training application built with Unity and SteamVR. Players solve three hands-on mechanical puzzles in virtual reality: a cable routing challenge, a gear assembly task, and a screw/cover maintenance procedure.

---

## Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Scenes](#scenes)
- [Puzzles](#puzzles)
- [Architecture](#architecture)
- [SteamVR Input & Controllers](#steamvr-input--controllers)
- [Building](#building)

---

## Overview

VR Training places the player in a scene containing three simultaneous puzzle stations. The player uses VR controllers to interact with physical objects, complete each puzzle, and receive visual feedback (particle effects, animated faces) on their progress.

| Puzzle | Task |
|--------|------|
| **Cable Puzzle** | Route and connect color-matched cables to their correct endpoints |
| **Gear Puzzle** | Pick up, stack, and seat gears onto the correct pins |
| **Cover / Screw Puzzle** | Remove screws with a screwdriver and lift the cover off a box |

Completing all three puzzles triggers a success state (happy face, particles stop). Unsolved puzzles continuously spray particles as a failure indicator.

---

## Prerequisites

| Requirement | Notes |
|-------------|-------|
| **Unity 2018.2.0f2** | Exact version required; download via [Unity Hub](https://unity3d.com/get-unity/download/archive) |
| **SteamVR runtime** | Install from Steam (free); must be running when launching in the editor |
| **A SteamVR-compatible VR headset** | HTC Vive, Valve Index, Oculus Rift/Quest (via SteamVR), or Windows Mixed Reality |
| **Windows 10 (64-bit)** | SteamVR is Windows-primary; macOS/Linux not officially supported |
| **Git** | To clone the repository |

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/omar92/VR-Training.git
cd VR-Training
```

### 2. Open in Unity

1. Launch **Unity Hub**.
2. Click **Add** and select the cloned `VR-Training` folder.
3. Choose **Unity 2018.2.0f2** as the editor version when prompted.
4. Open the project. Unity will import all assets and resolve package dependencies automatically.

### 3. Open the main scene

Navigate to **Assets/Main/main.unity** in the Project window and double-click to open it.

### 4. Start SteamVR

Make sure the SteamVR runtime is running and your headset is connected before pressing **Play** in the Unity editor.

### 5. Play

Press **Play** in the Unity editor. Put on your headset and use the controllers to interact with the puzzle objects.

---

## Project Structure

```
VR-Training/
├── Assets/
│   ├── CableComponent/        # Cable puzzle – physics, detection, prefabs, test scene
│   │   └── Scripts/
│   │       ├── CableComponent.cs    # Verlet-integration cable physics engine
│   │       ├── CableSystem.cs       # Puzzle completion logic
│   │       ├── CableDetection.cs    # Color-matched endpoint detection
│   │       └── CableParticle.cs     # Individual cable node
│   ├── Cover/                 # Screw/cover puzzle – scripts, prefabs, test scene
│   │   └── Scripts/
│   │       ├── CoverPuzzle.cs       # Cover puzzle controller
│   │       ├── ScrewScript.cs       # Screw interaction
│   │       └── ScrewTip.cs          # Screwdriver tip detection
│   ├── Scripts/               # Core gameplay scripts
│   │   ├── GameManager.cs           # Overall game state; success/failure feedback
│   │   ├── GearBox.cs               # Manages a collection of gears and pins
│   │   ├── GearRotation.cs          # Gear physics, angular velocity, motor relationships
│   │   ├── GearStack.cs             # Pick-up, snap, and seat gears onto pins
│   │   ├── GearColor.cs             # Material swap for rotation visual feedback
│   │   ├── GearPin.cs               # Anchor point for gear snapping
│   │   ├── SpurtObjects.cs          # Spawns failure-state particles continuously
│   │   └── SpurtingObject.cs        # Single particle lifecycle (auto-destroys after 10 s)
│   ├── ScriptableObject/      # Data-driven event & variable system
│   │   ├── Events/                  # GameEvent, GameEventListener, editor tools
│   │   └── Variables/               # FloatVariable, IntegerVariable, SnappingDistance asset
│   ├── EventsData/            # Puzzle success event assets
│   │   ├── puzzle1success.asset
│   │   ├── puzzle2success.asset
│   │   └── puzzle3success.asset
│   ├── Main/                  # Primary game scene and Player prefab
│   ├── Scenes/                # Isolated test scenes (gears, particles)
│   ├── Models/                # FBX / Blend 3-D models (gears, wrench, screw, spring)
│   ├── Material/              # Gear materials (moving, stopped, pin)
│   ├── Prefabs/               # Runtime prefabs (Gears, GearsBox, SpurtingObjects)
│   ├── SteamVR/               # SteamVR Unity plugin (bundled)
│   └── SteamVR_Input/         # Generated SteamVR input action assets
├── Packages/
│   └── manifest.json          # Unity Package Manager dependencies
├── ProjectSettings/           # Unity project configuration
├── actions.json               # SteamVR input action definitions
├── bindings_*.json            # Per-controller input bindings (4 devices)
└── unityProject.vrmanifest    # Steam VR manifest ("VR Training [Testing]")
```

---

## Scenes

| Scene | Path | Purpose |
|-------|------|---------|
| **main** | `Assets/Main/main.unity` | Production scene; all three puzzles, Player prefab, full game loop |
| **GearTestScene** | `Assets/Scenes/GearTestScene.unity` | Isolated gear mechanics testing |
| **ParticlesTest** | `Assets/Scenes/ParticlesTest.unity` | Particle / spurting-object effect testing |
| **testScene** | `Assets/Cover/testScene.unity` | Isolated screw/cover puzzle testing |
| **Simple Sample** | `Assets/SteamVR/…` | SteamVR plugin sample |

Start development in **main.unity**. Use the individual test scenes when working on a single puzzle system.

---

## Puzzles

### Puzzle 1 – Cable Routing

Scripts: `Assets/CableComponent/Scripts/`

The cable system simulates rope physics using **Verlet integration**. Each cable is a chain of `CableParticle` nodes. The `CableDetection` component checks whether a cable's free end overlaps a color-matched socket. `CableSystem` polls all cables and raises `puzzle1success` when every cable is connected.

### Puzzle 2 – Gear Assembly

Scripts: `Assets/Scripts/Gear*.cs`

Gears are rigid bodies sitting in a `GearBox`. `GearStack` enables the player to grab a gear, disables gravity while held, and snaps it onto a free `GearPin` using smooth damping interpolation. Once seated, `GearRotation` calculates angular velocity based on neighboring gears (motor–driven or collision–driven). `GearColor` swaps materials to show whether a gear is currently spinning. `GameManager` raises `puzzle2success` when all gears are placed and spinning.

### Puzzle 3 – Cover / Screw

Scripts: `Assets/Cover/Scripts/`

The player uses the VR screwdriver tool (`ScrewTip` detects contact with screw heads via `ScrewScript`) to remove all screws. Once all screws are removed, `CoverPuzzle` allows the cover to be lifted. `GameManager` raises `puzzle3success` on completion.

---

## Architecture

### ScriptableObject Event System

Puzzles communicate with the rest of the game through a lightweight **ScriptableObject event bus** (namespace `Raskulls.Events`):

```
GameEvent (asset)          ← puzzle1success.asset, puzzle2success.asset, etc.
    └─ Raise()             ← called by puzzle scripts on completion
GameEventListener          ← attached to GameObjects; listens to a GameEvent asset
    └─ OnEventRaised       ← UnityEvent wired in the Inspector
```

This pattern keeps puzzle scripts decoupled from `GameManager` and UI objects.

### ScriptableObject Variables

Shared mutable values (e.g., snapping distance) are stored as `FloatVariable` / `IntegerVariable` assets in `Assets/ScriptableObject/Variables/`. Scripts hold a reference to the asset rather than a hard-coded value, making tuning easy from the Inspector.

### Game Loop

```
Player enters scene
    │
    ├─ Cable puzzle (CableSystem) ──► puzzle1success raised
    ├─ Gear puzzle  (GearBox)    ──► puzzle2success raised
    └─ Cover puzzle (CoverPuzzle)──► puzzle3success raised
                                          │
                                    GameManager
                                    ├─ All done → Happy face + stop particles
                                    └─ In progress → Angry face + spray particles
```

---

## SteamVR Input & Controllers

Input is handled by the **SteamVR Input system** (SteamVR Unity Plugin 2.x). Action definitions live in `actions.json`; per-device bindings are in the four `bindings_*.json` files.

| Device | Binding file |
|--------|-------------|
| HTC Vive | `bindings_vive_controller.json` |
| Oculus Touch | `bindings_oculus_touch.json` |
| Valve Index Knuckles | `bindings_knuckles.json` |
| Windows Mixed Reality | `bindings_holographic_controller.json` |

### Defined actions (default action set)

| Action | Type | Usage |
|--------|------|-------|
| `InteractUI` | Boolean | UI pointer interaction |
| `Teleport` | Boolean | Teleportation trigger |
| `GrabPinch` | Boolean | Grip objects with finger pinch |
| `GrabGrip` | Boolean | Grip objects with full hand grip |
| `Pose` | Pose | Hand position/rotation tracking |
| `SkeletonLeftHand` / `SkeletonRightHand` | Skeleton | Finger articulation |
| `Squeeze` | Single (analog) | Squeeze force |
| `Haptic` | Vibration | Controller rumble feedback |

To modify bindings, open **Window → SteamVR Input** in the Unity editor and use the graphical binding editor, or edit the JSON files directly.

---

## Building

1. Open **File → Build Settings**.
2. Click **Add Open Scenes** (or manually add `Assets/Main/main.unity`).
3. Set **Platform** to **PC, Mac & Linux Standalone**, Architecture **x86_64**.
4. Click **Player Settings** and confirm:
   - Company/Product name matches your target.
   - **Virtual Reality Supported** is enabled under XR Settings (it should already be set).
5. Click **Build** and choose an output folder.
6. Distribute the resulting `.exe` together with the `_Data` folder. SteamVR must be installed on the target machine.

---

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| com.unity.textmeshpro | 1.2.4 | High-quality UI text |
| com.unity.postprocessing | 2.0.16-preview | Post-processing camera effects |
| com.unity.analytics | 2.0.16 | Unity Analytics |
| SteamVR Unity Plugin | 2.x (bundled) | VR headset and controller integration |

All other dependencies are standard Unity modules (`com.unity.modules.*`) included with Unity 2018.2.0f2.
