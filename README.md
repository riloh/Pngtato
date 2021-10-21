
# Avatar pop-up
 
A small Unity application for displaying "reactive" PNG's for use as a vtuber streaming avatar.  Current methods to achieve this appear to mainly revolve around setting up a discord server with a voice channel, and installing plugins, but I wanted something a little more standalone.

This is mainly a personal project for my own use, so don't expect super active development, but please feel free to open an issue or reach out elsewhere with any suggestions or bugs.
## Download

Windows: [Download](https://github.com/dlance2005/Avatar-pop-up/raw/main/finalBuild/build_win.zip)  


  
## Usage

Place two similarly sized PNG images in the program's directory, one for the image that will be displayed when idle, and another for when talking, named "resting.png" and "talking.png".  Optionally, a third PNG titled "blinking.png" can be included for a blinking effect (ideally for eye blinking.)

The following settings can be changed:

- Input device
- Sensitivity
- Background color
- Delay time on reverting back to resting PNG after talking
- Whether or not the bouncing animation plays when talking

Press \<space\> to hide/unhide the UI.
## To-do

- ~~Add setting to modify the delay of when avatar reverts to idle picture after talking stops.~~ DONE
- ~~Add support for "blinking" effect~~ DONE
- ~~Add option to disable bouncing effect~~ DONE
- Add support for multiple expressions (Almost done)


\
\
Makes use of UnityRawInput by Elringus: [link](https://github.com/Elringus/UnityRawInput)
\
This project is licensed under the terms of the MIT license.