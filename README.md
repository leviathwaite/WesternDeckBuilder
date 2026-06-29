# WesternDeckBuilder — Dust County

A **Western-themed roguelite deckbuilder** built in Unity.  
Players prepare for high-risk missions by building loadouts, navigating a county map, and assembling card synergies around weapons, cover, and positioning.

---

## Architecture Overview

The project uses a **bootstrap + service registry** pattern.  
All core systems register themselves on startup via `GameBootstrap` and are resolved at runtime through `ServiceRegistry` / `Services`.  
**No inspector drag-and-drop is required for any core flow system.**

### Service Locator Pattern

```
ServiceRegistry  ←  stores service instances by interface type
Services         ←  static convenience accessors (Services.Heat, Services.UIFlow, …)
GameBootstrap    ←  MonoBehaviour that creates + registers all services on Awake
```

Any MonoBehaviour can call `Services.Heat.IncreaseHeat(20)` without holding an inspector reference.  
`ServiceRegistry.Instance.Resolve<T>()` logs a clear error if a service is missing.

---

## Folder Structure

```
Assets/
  _Game/
    Core/
      Bootstrap/    → ServiceRegistry, Services, GameBootstrap, SceneNames
      Interfaces/   → IRunManager, IHeatManager, IUIFlowService, IInputRouter,
                       IRewardService, IDataRegistry
      Services/     → RunManager, HeatManager, UIFlowService, InputRouter, RewardService
      Validation/   → StartupValidator
    Data/           → ScriptableObject definitions
                       CardData, WeaponData, KeepsakeData, EnemyData,
                       NodeData, MissionStepData, RewardTableData, DataRegistry
    Models/
      Enums/        → NodeType, EncounterType, PositionState, HeatTier,
                       CardLifetime, WeaponSlotType
      HeatTierUtility.cs
    Features/
      Boot/         → BootController
      Loadout/      → LoadoutController
      Map/          → MapController
      Battle/       → BattleController
  Resources/
    Data/
      Cards/        ← place CardData .asset files here
      Weapons/      ← place WeaponData .asset files here
      Keepsakes/    ← place KeepsakeData .asset files here
      Enemies/      ← place EnemyData .asset files here
      Nodes/        ← place NodeData .asset files here
      MissionSteps/ ← place MissionStepData .asset files here
      RewardTables/ ← place RewardTableData .asset files here
```

---

## Scene Flow

```
Boot  →  Loadout  →  Map  →  Battle (stub)  →  Map
```

All scene name strings are defined as constants in `SceneNames.cs`.

---

## Creating the Boot Scene Object

1. Open Unity and create a new scene. Name it **`Boot`**.
2. Create an empty GameObject. Name it **`[GameBootstrap]`**.
3. Attach the **`GameBootstrap`** script to it.
4. Open **File → Build Settings → Scenes In Build** and add all four scenes in this order:
   - `Boot` (index 0)
   - `Loadout`
   - `Map`
   - `Battle`
5. Press **Play** from the Boot scene. `GameBootstrap.Awake` will:
   - Load all data assets from `Resources/Data/`.
   - Register all services.
   - Automatically navigate to the Loadout scene.

No additional inspector assignments are needed.

---

## Required Package: Unity Input System

`InputRouter` wraps Unity's **Input System** package for device-aware input events.

### Installation

1. Open **Window → Package Manager**.
2. Search for **Input System** and install version **1.7.0** or later (already declared in `Packages/manifest.json`).
3. When prompted, choose **Yes** to switch to the new input backends, **or** set
   **Project Settings → Player → Active Input Handling** to **Both** to keep legacy input working too.

If the package is not installed, `InputRouter` falls back to Unity's legacy `Input.GetButtonDown` for Submit and Cancel only.

---

## Adding Data Assets

1. Right-click inside the **Project** window under `Assets/Resources/Data/<Type>/`.
2. Select **Create → WesternDeckBuilder → Data → \<Type\>**.
3. Fill in the **`ID`** field with a unique snake_case string (e.g. `card_double_barrel_001`).
4. Fill in all other required fields.

> **Important:** Every asset's `ID` field must be unique within its type.  
> IDs are the sole key used for all runtime cross-references and save data.

### Asset ID conventions

| Type | Example ID |
|---|---|
| CardData | `card_quickdraw_001` |
| WeaponData | `weapon_basic_pistol` |
| KeepsakeData | `keepsake_sheriffs_badge` |
| EnemyData | `enemy_bandit_grunt` |
| NodeData | `node_dusty_creek_hideout` |
| MissionStepData | `mission_train_boarding` |
| RewardTableData | `rewards_battle_win` |

---

## Running Startup Validation

Validation runs automatically on `GameBootstrap.Awake()`. Check the Unity **Console** window for:

| Log prefix | Meaning |
|---|---|
| `[DataRegistry] Duplicate ID …` | Two assets share the same ID — rename one. |
| `[DataRegistry] … has no ID set` | An asset's `ID` field is blank — fill it in. |
| `[DataRegistry] … not found for id '…'` | A cross-reference points to a missing asset ID. |
| `[StartupValidator] Startup validation complete` | All checks passed (warnings may still appear above). |

---

## Platform Support

| Platform | Input | Notes |
|---|---|---|
| Android (Portrait) | Touch | Primary target. Single-thumb layout. |
| PC | Mouse + Keyboard | Full cursor support. |
| PC / Console | Gamepad (Xbox / generic) | Focus navigation and button mapping. |

`InputRouter` detects the active device and fires `IInputRouter.OnDeviceChanged` so any UI panel can swap prompts dynamically without polling.

---

## Key Design Decisions

- **Keepsakes** — relic-equivalent items. Sub-typed as `TrailToken` or `Badge` for flavor.
- **Card lifetimes** — `Permanent`, `OneTimeUse`, `SingleOpportunity`, `UseItOrLoseIt`, `Battlebound`, `Handbound`.
- **Heat system** — raw value 0–150 mapped to tiers H0 (Cold) … H5 (Dead or Alive). Decays 8/node; resets at hideout.
- **Extension points** — Synty Western, Mixamo, and Epic Toon FX assets can be integrated via prefab key lookups in `EnemyData.PrefabKey` and future `EnvironmentData` ScriptableObjects without changing core logic.

---

## Unity Version

This project targets **Unity 2022.3 LTS**. Open the project in Unity Hub using version 2022.3.x.