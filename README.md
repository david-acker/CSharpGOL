# CSharpGOL
C# Implementation of Conway's Game of Life

## Background
Implements the standard set rule of the [The Game of Life], the cellular automaton created by the late mathematician [John Horton Conway].

## Usage
Once launched, CSharpGOL automatically determines the dimensions (height and width) of the terminal it was run in, and creates randomly an initial grid using these dimensions. The initial grid is populated by randomly setting cells as either alive or dead.

Internally, the state of the grid is stored as a 2D boolean array in which True indicates a living cell and False indicates a dead cell. For the purpose of implementing the game rules, the boundaries of the grid wrap around to the opposite side.

Each generation, the state of the current grid is converted to a display string and printed to the console. A rudimentary timing mechanism is used to trigger an update of the display approximately every 100ms, to ensure a relatively uniform "frame rate" to provide continuity and prevent visual glitching/flashing. The number for the current generation is centered and displayed on a header row at the top of the terminal screen.

To end the simulation and exit, use CTRL-C. CSharpGOL does not currently have an automatic exit mechanism to detect when the grid for the simulation becomes completely static and unchanging, or becomes comprised only of still life and/or oscillator patterns. 

Note: Thus far, CSharpGOL has only been tested with the default GNOME Terminal for Ubuntu Desktop (Focal Fossa) and, therefore, may be displayed differently on other platforms.  

![](https://github.com/david-acker/CSharpGOL/blob/main/CSharpGOL.gif)

[The Game of Life]: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
[John Horton Conway]: https://en.wikipedia.org/wiki/John_Horton_Conway
