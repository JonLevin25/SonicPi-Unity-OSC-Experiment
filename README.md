# SonicPi-Unity-OSC-Experiment
An experiement playing around with OSC, Sonic Pi and Unity.
Made for the TAMI exhibition fundraiser

### Prerequisites:
* Clone/download repo
* Install Sonic Pi, and Unity 2020.3 (any 2020/2021 will *probably* work, older/newer not guarenteed)
* Open `Unity-OSC-Receiver` project in Unity.

### UNITY NOTES:
* Controls: WASD - move. Mouse - look
* Make sure the "Maximize on play" in the game window button is pressed for the full experience.
* Once playing, the cursor will disappear. Press `ESC` to free the mouse, and click on the game view to go back in
* Use Unity's play button (top-middle of the screen) to enter/exit play mode, or `Ctrl+P`

## Demos
### Running the four seasons demo:
1. In **Unity**
    * Open `Assets/Scenes/FourSeasons_Scene.unity`
    * Enter play mode
2. In **Sonic Pi**
    * Stop any playing buffers
    * Load `sonic-pi-main.rb` into an empty buffer.
    * Run the buffer
3. You should hear the music, and the trees and planes should react to it.

### Running the tribal demo
1. In **Unity**
    * Open `Assets/Scenes/Tribal_Scene.unity`
    * Enter play mode
2. In **Sonic Pi**
    * Stop any playing buffers
    * Load `sonic-pi-tribal.rb` into an empty buffer
    * **Edit `#LIB_ROOT = <PATH_TO_THIS_SCRIPTS_FOLDER>` to the relevant path**
    * (For windows paths - directory separators should be either `\\\\` or `/` (double backslash or a single forward-slash)
    * Run the buffer
3. You should hear the music, and the trees and planes should react to it.


### DISCLAIMER
*Any features may be buggy*<br />
*Any bugs may be features*<br />
*Any code may be both hacky **and** over-engineered at the same time*<br />
*Any descriptions may be confusing and any confusion may be descriptive :)*<br />
