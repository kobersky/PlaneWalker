![Image Alt text](/Images/pw.png)


Game Overview:
------------------
Pick up the key in order to unlock the exit of each level.
Collect coins and avoid traps. 

Gameplay Controls:
------------------
- Up|Down|Left|Right Arrows [Keyboard] / Left Stick Gamepad] : Move
- Space [Keyboard]/Button South (A) [Gamepad] : Jump
- X [Keyboard]/Left Trigger [Gamepad] : Switch Cameras
- W|A|S|D Buttons [Keyboard] / Right Stick [Gamepad]  : Rotate Camera (when in free camera mode only)

Level Editing:
--------------
- Under "Assets/Prefabs", open "LevelEditor.prefab", which allows creating new levels.
- Choose the intended action ("Delete"/"Create Platform", etc.) from the script's "Action Type" drop-down menu, and execute it by clicking on it's intended position in the scene (when still in prefab mode).
- Clicking on "Save New Level", will result in the level being saved into "Assets/Prefabs/Levels" directory.
- Clicking on "Reset Level", will result in removing all objects besides the base surface and the bounds of the level.
- Drag the newly created level into MainScene's "GameManager" GameObject, under "LevelsList" field, and the new level will become available for playing from the game's main menu.
- While using this tool, please don't add/delete gameobjects from the hierarchy (rather, use the add/delete actions from the drop down menu, as mentioned above).
