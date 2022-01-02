# CSharpGOL
C# Implementation of Conway's Game of Life

## Background
The [The Game of Life] is the [cellular automaton] created by the late mathematician [John Conway].

Using a 2D grid, each square on the grid is considered a cell which can be in one of two possible states: live or dead. Over successive generations, the state of a given cell changes based on the eight neighboring cells in the cell's immediate surroundings:

* A live cell that has two or three live neighbors survives
* A dead cell with exactly three live neighbors becomes a live cell
* All other live cells die out
* All other dead cells remain dead

## Command Line Options
Option | Parameter | Type | Default
-------|-----------|------|--------
--height|grid height cells|int|*dependent on terminal window size*
--width|grid width in cells|int|*dependent on terminal window size*
--refresh|minimum time between frames in milliseconds|int|100
--auto|if the simulation will automatically progress|bool|true

## Command Line Options
Option | Parameter | Type | Default
-------|-----------|------|--------
--height|The height of the cell grid.|int|*dependent on terminal window size*
--width|The width of the cell grid.|int|*dependent on terminal window size*
--refresh-rate|The minimum time between frames, in milliseconds.|int|50
--living-cell|The character displayed for a living cell.|char|'X'
--dead-cell|The character displayed for a dead cell.|char|' '
--header|The character displayed for the header bar.|char|'_'
--show-header|Whether to display the generation number header.|bool|true
--look-back-window|The number of previous frames included when checking if the simulation has reached a steady state.|int|5
--loop|Whether to restart the simulation from its initial state, once the simulation has reached a steady state.|bool|false
--density|The density of living cells when randomly generating the cell grid. Ranges from 1 (10%) to 9 (90%).|int|5
--randomize-density|Whether to use a random density every time the simulation restarts.|bool|true
--seed|An optional integer for seeding the random number generator.|int|*uses default system seed*

Note: The default values for the command line options are set in 'appsettings.json'.

---

<p align="center">
  <img width=100% height=auto src="https://github.com/david-acker/CSharpGOL/blob/main/CSharpGOL.gif">
</p>

[The Game of Life]: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
[cellular automaton]: https://en.wikipedia.org/wiki/Cellular_automaton
[John Conway]: https://en.wikipedia.org/wiki/John_Horton_Conway
