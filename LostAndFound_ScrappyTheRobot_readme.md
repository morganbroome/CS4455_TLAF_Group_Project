# CS4455_TLAF_Group_Project

# **i. Start scene file**
"GameMenuScene_Profiles"
# **ii. How to play and what parts of the level to observe technology requirements**
## 3D Feel Game
- Players can win the game by collecting all 31 coins
- Can press 'Esc' to bring up game menu
- Can reference controlls in the game menu

## Goals/Sub Goals
Main Puzzles
- Pillars
- Movable boxes
- Castle
- Wall
- Rocket
- Cave puzzle
- Destroyable boxes
- Various other small puzzles

Getting to the end of these puzzles will reward you with coins. When players collect all the coins they will win the game and be brought to the win screen. If a player 
is killed by an enemy, they fall off the map, or they touch a dangerous object such as a rolling boulder, they will be sent back to the spawn point but their progress 
will remain.

## 3D Character/Real Time Control
Controls
- Up Key or Joystick Up: Move forward
- Down Key or Joystick Down: Move backwards
- Left Key or Joystick Left: Move left
- Right Key or Joystick Right: Move right
- Left Click or Right Trigger: Shoot one bullet
- Right Click or Left Trigger: Shoot three bullets
- Middle Mouse or Left Bumper: Turn on laser pointer
- Space bar or South Controller Button: Jump (Double press for double jump)
- Move mouse or Right stick: Rotate left and right or up and down
- 'Esc' Key or Start: Menu

Navigation of the UI requires a mouse

Keys to Help TAs
- 'Shift' + 'B' Key: Teleport to the Rocket ship
- 'Shift' + 'N' Key: Teleport to top of Wall
- 'Shift' + 'M' Key: Teleport to Origin


## 3D World with Physical/Spatial Simulation
- Elevator that starts when stepped on(can be found at rocketship)
- Boulders that roll from castle entrance
- Destroyable crates(Can be found at starting point)
- Movable boxes
- Red button to open starting doors
- Sound played when gun is shot, when jumping, when dying, and when picking up coins


## AI/ RealTimeNPC
- Can find enemies with AI right outside of starting area
- If an enemy touches scrappy he will be sent back to spawn point

## Polish
- Game can be paused with 'Esc' key and sensitivity slider
- Particle effect when coin is collected
- Textures
- In game world border that prevents the character from falling off the map (mostly)
- Swaying grass

# **iii. Known problem areas**
- Enemy AI

# **iv. Manifest of which files authored by each teammate:**

## Nifemi Bolu
### What did I do?
Implemented 
- Built the gun, in addition to shooting mechanics and effects
- Level Design: Crate Wall, Caves
- Contributed to puzzles ex: Cave Puzzle,Rocketship Puzzle, Castle Puzzle
- The elevator animations
- Button Press to open door
- Sound effects: Background Music, Jump sound, shooting sound, dying sound



### Assets Implemented
- Gun Prefab
- Bullet Prefab
- attackSphere
- elevator
- Container Crates
- Cave 
- Doors


### C# Scripts Built
- Gun
- LaserToggle
- ballLife
- BulletController
- spawnBall
- arrowMover
- DoorOpen
- startElevator


## Guy Broome
### What did I do?
Implemented
- Base Character and Level Design
- GUI (Except for Collectibles)
- Started Controller Integration (Not Finished)
- Character Movement adjustments
- Controller Implementation
- Small Contribution to Puzzles
- Character Camera Ajustments

### Assets Implemented
- Player Model
- General Level Design (Great Wall, Starting Area, Rocket Ship, Ground, Ridges)
- Starting Text and GUI For Starting the Game and Pause Menu
- Kill box below the map

### C# Scripts Built
- PlayerController (Movement)
- GameQuitter
- GameStarter
- PauseMenu Toggle
- StartText
- Static Camera for Debugging

Ben Lathrop
### What did I do?
- Camera functionality
- Character movement tweaking
- Character/Camera rotation functionality
- AI Implementation fixing
- Character Animation

### Assets Implemented
- Character Animation

### C# Scripts Built
- PlayerController
- EnemyStateManager


Courage Agabi
### What did I do?
- AI Implementation

### Assets Implemented

### C# Scripts Built
- EnemyStateManager
- EnemyNavigation

