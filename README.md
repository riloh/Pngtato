
# Pngtato
 
A small Unity application for displaying "reactive" PNG's for use as a pngtuber streaming avatar.  Current methods to achieve this appear to mainly revolve around setting up a discord server with a voice channel, and installing plugins, but I wanted something a little more standalone.

This is mainly a personal project for my own use, so don't expect super active development, but please feel free to open an issue or reach out elsewhere with any suggestions or bugs.
## Download
NOTE: Emote switching while application is not in focus is currently only supported on Windows for now.  Switching emotes on Linux must be done with application in focus.  Will be looking to support this on non-Windows hosts in the future.
\
\
Windows: [Download](https://github.com/riloh/Pngtato/raw/main/finalBuild/build_win.zip)  
Linux: [Download](https://github.com/riloh/Pngtato/raw/main/finalBuild/build_linux.zip)  

## Usage

Place two identically sized PNG images in the images directory, one for the image that will be displayed when idle, and another for when talking, named "resting.png" and "talking.png".  Optionally, a third PNG titled "blinking.png" can be included for a blinking effect (ideally for eye blinking.)  There's also support for up to five emotes by providing up to five PNG's, named "emote\[1-5\].png", which can be switched between using configurable keybindings.

The following settings can be changed:

- Input device
- Sensitivity
- Background color
- Delay time on reverting back to resting PNG after talking
- Whether or not the bouncing animation plays when talking
- Keybinds for switching emotes

Press \<space\> to hide/unhide the UI.
## To-do

- ~~Add setting to modify the delay of when avatar reverts to idle picture after talking stops.~~ DONE
- ~~Add support for "blinking" effect~~ DONE
- ~~Add option to disable bouncing effect~~ DONE
- ~~Add support for multiple expressions~~ DONE


\
\
Makes use of UnityRawInput by Elringus: [link](https://github.com/Elringus/UnityRawInput)
\
\
This project is licensed under the terms of the MIT license.