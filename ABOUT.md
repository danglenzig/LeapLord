# Leap Lord — Project Overview (Unity 6)

**Project type**: 2.5D platformer inspired by Jump King (focused, one-level scope, charge-and-jump core mechanic)

**Unity version**: 6000.2.6f2

**Primary scenes**:

\- `Assets/Scenes/Start.unity`: Start/Menu


\- `Assets/Scenes/Lab.unity`: Gameplay level

## Summary

Leap Lord is a small, systems-focused recreation inspired by Jump King. The emphasis is on a single, polishable level and a single core mechanic: charging a jump and committing to its outcome. There is minimal or no mid-air control; skill comes from gauging charge strength and route planning.

---

## Systems Overview

### Input System

\- **Tech**: Unity Input System
\- **Runtime owner**: `InputHandler` singleton
\- **Role**: Centralizes player input actions (move, jump press/hold/release). Exposes a clean API to gameplay systems without scattering input logic across scripts.
\- **Script**: `Assets/Scripts/Singletons/InputHandler.cs`.



\### 2) Player Controller and State Machine

\- \*\*Core object\*\*: `Player` prefab driven by a lightweight finite state machine.

\- \*\*State framework\*\*: `SimpleStateMachine`, `State`, `StateTransition`.

\- \*\*Player state handlers\*\*: Govern the behavior of the player objects in the various states.

`IdleStateHandler`, `WalkStateHandler`, `JumpPrepStateHandler` (charge jump), `AirborneStateHandler` (execute jump), `ParkedStateHandler` -- all inherit from the abstract class StateHandler and have override methods to deal with the various callbacks that govern gameplay: HandleOnEnter(), HandleOnExit(), HandleUpdate(), HandleFixedUpdate(), HandleJumpPressed(), HandleJumpReleased()


\- \*\*Key scripts\*\*: 

&nbsp; - Player: `Assets/Scripts/Player/Player.cs`

&nbsp; - States: `Assets/Scripts/Player/\*.cs`

&nbsp; - State framework: `Assets/Scripts/StateMachine/\*.cs`

\- \*\*Mechanic\*\*: Jump strength accumulates while holding jump. On release, the state machine applies an impulse based on charge and hands off to the airborne state. Horizontal control during flight is limited or disabled to preserve the Jump King feel.



\### 3) Physics and Movement

\- \*\*Physics\*\*: Rigidbody-based motion and collisions; materials configured for character and surfaces.

\- \*\*Forces\*\*: Impulse on jump start; gravity dominates during airborne. Friction/bounciness via physics materials as needed.

\- \*\*Camera\*\*: See Camera System below.



\### 4) Camera System

\- \*\*Scripts\*\*: `CameraMover`, `CameraTrigger`.

\- \*\*Behavior\*\*: Follows the player within zones, with triggers to constrain/snap camera or adjust framing when entering new areas.

\- \*\*Goal\*\*: Keep platforming challenges readable while showcasing vertical progression.



\### 5) Gameplay Environment

\- \*\*Scenes\*\*: `Start.unity` (menu), `Lab.unity` (gameplay).

\- \*\*Prefabs\*\*: Player, managers, checkpoints, VFX, and re-usable level pieces.

\- \*\*Examples\*\*: `Assets/Prefabs/Player/Player.prefab`, `Assets/Prefabs/Checkpoint.prefab`, `Assets/Prefabs/PlayerManager.prefab`, `Assets/Prefabs/TeleportEffectPrefab.prefab`.

\- \*\*Approach\*\*: Assemble the level by placing prefabs (platforms/props/hazards), keeping authoring simple and modular.



\### 6) Collectibles / Score / Checkpoints

\- \*\*Script\*\*: `CheckpointGem` handles collectible interaction.

\- \*\*Design\*\*: Gems can serve as progress markers and/or score increments. They communicate with manager/systems to update state.

\- \*\*Scripts\*\*: `Assets/Scripts/Collectibles/CheckpointGem.cs`.



\### 7) UI and HUD

\- \*\*Jump Charge Bar\*\*: `JumpStrengthProgressBar` displays current jump charge to support the commit-to-jump mechanic.

\- \*\*Narration/Tutorial\*\*: `NarrationUI`, `TutorialUI` with corresponding data assets to present onboarding and flavor text.

\- \*\*Button handling\*\*: `ButtonScript` for menu interactions.

\- \*\*Scripts\*\*: `Assets/Scripts/Player/JumpStrengthProgressBar.cs`, `Assets/Scripts/UI/NarrationUI.cs`, `Assets/Scripts/UI/TutorialUI.cs`, `Assets/Scripts/UI/ButtonScript.cs`.

\- \*\*Text rendering\*\*: TextMesh Pro with custom fonts in `Assets/Fonts`.



\### 8) Menus and Scene Management

\- \*\*Start menu\*\*: Implemented in `Start.unity`, wired via `ButtonScript` and manager calls.

\- \*\*Gameplay scene\*\*: `Lab.unity` is the main level.

\- \*\*Scene registry\*\*: `Helpers/SceneNames.cs` provides a central enum/const source for scene references.

\- \*\*Scripts\*\*: `Assets/Scripts/Helpers/SceneNames.cs`, `Assets/Scripts/Singletons/GameManager.cs`.



\### 9) Singletons / Managers

\- \*\*GameManager\*\*: Global game state (score/progress, scene flow).

\- \*\*PlayerManager\*\*: Central place for player reference/spawn and cross-scene persistence where needed.

\- \*\*InputHandler\*\*: Centralized input (see Input System).

\- \*\*Prefabs/Scripts\*\*: `Assets/Prefabs/PlayerManager.prefab`, `Assets/Scripts/Singletons/\*.cs`.



\### 10) Animation and VFX

\- \*\*Animation\*\*: `QuadSpriteAnimator` supports simple sprite/quads-based character animation.

\- \*\*VFX\*\*: Teleport/transition effects via `TeleportEffect` and `TeleportEffectPrefab` plus materials/shaders.

\- \*\*Scripts\*\*: `Assets/Scripts/Player/QuadSpriteAnimator.cs`, `Assets/Scripts/Player/TeleportEffect.cs`.



\### 11) Utilities and Helpers

\- \*\*Helpers\*\*: `EnumLeoAnimations`, `Tags`, `NarrationNames` — central enums/constants for consistency.

\- \*\*Motion utilities\*\*: `FloatTweener`, `Oscillator` for simple time-based effects.

\- \*\*Scripts\*\*: `Assets/Scripts/Helpers/\*.cs`, `Assets/Scripts/Misc/\*.cs`.



---



\## Key Assets and Folders

\- `Assets/Scenes/` — `Start.unity` (menu), `Lab.unity` (level), post-process profiles for look.

\- `Assets/Prefabs/` — `Player`, `PlayerManager`, `InputHandler`, `Checkpoint`, `TeleportEffectPrefab`, plus materials/shaders for the player quad.

\- `Assets/Scripts/` — Organized by domain: `Player`, `StateMachine`, `Singletons`, `UI`, `Camera`, `Collectibles`, `Helpers`, `Misc`.

\- `Assets/Fonts/` — Custom font assets for UI (used with TextMesh Pro).

\- `Assets/Resources/` — Sprites/textures and UI assets for runtime loading as needed.



---



\## How to Play (Editor)

1\) Open `Assets/Scenes/Start.unity` and press Play.

2\) From the Start screen, begin the game to load `Lab.unity`.

3\) Use keyboard/controller per Input System bindings to move, hold jump to charge, and release to commit to the jump.

4\) Watch the jump bar for charge strength. Collect gems/checkpoints as you progress upward.



---



\## Future Work / Nice-to-haves

\- Add a pause menu (resume/restart/options).

\- Add sound effects and simple background music.

\- Expand hazard variety and add a small enemy type with patrol AI.

\- Add optional power-up that temporarily modifies jump charge rate or max strength.

\- Polish camera transitions across tall vertical sections.



---

