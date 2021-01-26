using CustomExtensions;
using System.Linq;
using System;

namespace CSharpGOL
{
    public class Renderer
    {
        const char iconLive = '\u25a0';
        const char iconDead = '\u2800';

        const char headerBase = '\u2581';
        const char footerBase = '\u2594';

        private readonly int rowSize;
        private readonly int colSize;
        private readonly int displayWidth;

        private string header;
        private string footer;

        private Simulation simulation;

        public Renderer(ref Simulation simulation)
        {
            this.simulation = simulation;

            this.rowSize = simulation.rowSize;
            this.colSize = simulation.colSize;

            this.displayWidth = (colSize * 2);
            footer = String.Concat(
                Enumerable.Repeat(footerBase, displayWidth));
        }

        public void RefreshFrame()
        {
            string frame = "";
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

            header = ConstructHeader(
                simulation.generation.ToString(), displayWidth, headerBase) + "\n";

            Console.WriteLine(header + frame + footer);
        }

        public string ConstructHeader(string stringToDisplay, int totalWidth, char headerBase)
        {
            var innerBlock = stringToDisplay.PadCenter(stringToDisplay.Length + 2);
            
            return innerBlock.PadCenter(totalWidth, headerBase);
        }
    }
}