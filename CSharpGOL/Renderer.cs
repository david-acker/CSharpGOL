using System;
using System.Linq;
using CSharpGOL.Common;

namespace CSharpGOL
{
    /// <summary>Produces graphical representations of the Grid from a Simulation</summary>
    public class Renderer
    {
        const char iconLive = '\u25a0';
        const char iconDead = '\u2800';

        const char borderBase = '\u2501';

        private readonly int rowSize;
        private readonly int colSize;
        private readonly int displayWidth;

        private string header;
        private string footer;

        private string currentFrame;

        private Simulation simulation;

        /// <summary>Construct a Renderer instance for a provided Simulation</summary>
        public Renderer(Simulation simulation)
        {
            this.simulation = simulation;

            rowSize = simulation.rowSize;
            colSize = simulation.colSize;

            displayWidth = (colSize * 2);
            footer = String.Concat(
                Enumerable.Repeat(borderBase, displayWidth));
        }

        /// <summary>Creates a visual representation of the current grid state by cell</summary>
        public void DrawFrame()
        {
            var frame = "";
            for (int i = 0; i < rowSize; i++)
            {
                string line = "";
                for (int j = 0; j < colSize; j++)
                {
                    bool cellState = simulation.currentGrid.state[i, j];

                    if (cellState)
                        line += iconLive + " "; 
                    else
                        line += iconDead + " ";
                }
                line = line.Trim();
                frame += line + "\n";
            }
            currentFrame = frame;
        }

        /// <summary>Displays the current frame combined with the header and footer</summary>
        public void RefreshFrame()
        {
            DrawFrame();

            header = ConstructHeader(
                simulation.generation.ToString(), 
                displayWidth, 
                borderBase);
            
            Console.WriteLine(header + "\n" + currentFrame + footer);
        }

        /// <summary>Create a header given a display string, header width, and header character</summary>
        public string ConstructHeader(string stringToDisplay, int totalWidth, char headerBase)
        {
            // Put a buffer of one space around display string for readability
            var innerBlock = stringToDisplay.PadCenter(stringToDisplay.Length + 2);
            
            return innerBlock.PadCenter(totalWidth, headerBase);
        }
    }
}