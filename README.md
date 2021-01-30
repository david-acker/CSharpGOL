# CSharpGOL
C# Implementation of Conway's Game of Life

## Background
The [The Game of Life] is the [cellular automaton] created by the late mathematician [John Conway].

Using a 2D grid, each square on the grid is considered a cell which can be in one of two possible states: live or dead. Each generation the grid is updated, the cells 'interact' with the eight neighbor cells immediately surrounding them and change state according to the following rules:

* A live cell that has two or three live neighbors survives
* A dead cell with exactly three live neighbors becomes a live cell
* All other live cells die out
* All other dead cells remain dead

## Usage
Once launched, CSharpGOL automatically determines the dimensions (height and width) of the terminal instance it was in which it was run and generates a grid to match these dimensions. The height and/or width dimensions can also be explicity specified using their corresponding command line options. This The individual cells in this starting grid are randomly set as live or dead, to create the initial state for the simulation.

Internally, the state of the grid is stored as a 2D boolean array, where True and False correspond to live and dead cells respectively. When calculating the neighbors of for the the boundaries of the grid wrap around to the opposite side.

Each generation, the state of the current grid is converted to a display string and printed to the console. A rudimentary timing mechanism is used to trigger an update of the display roughly every 100 ms to show show the evolution of the simulation over each successive generation and to ensure a relatively uniform "frame rate" (in order to prevent visual glitches/flashing). Optionally, this refresh period can be explicitly specified using its command line option. The number for the current generation is centered and displayed on a header row at the top of the terminal screen.

To end the simulation and exit, use CTRL-C. CSharpGOL does not currently have an automatic exit mechanism to detect when the grid for the simulation becomes completely static and unchanging, or becomes comprised only of still life and/or oscillator patterns. 

Note: Thus far, CSharpGOL has only been tested with the default GNOME Terminal for Ubuntu Desktop (Focal Fossa) and may be displayed differently on other platforms.  

## Command Line Options
Option | Parameter | Type | Default
-------|-----------|------|--------
--height|grid height cells|int|*dependent on terminal window size*
--width|grid width in cells|int|*dependent on terminal window size*
--refresh|minimum time between frames in milliseconds|int|100

---

<p align="center">
  <img width=100% height=auto src="https://github.com/david-acker/CSharpGOL/blob/main/CSharpGOL.gif">
</p>

[The Game of Life]: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
[cellular automaton]: https://en.wikipedia.org/wiki/Cellular_automaton
[John Conway]: https://en.wikipedia.org/wiki/John_Horton_Conway
