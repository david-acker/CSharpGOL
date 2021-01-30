using CSharpGOL.Common;

namespace CSharpGOL
{
    /// <summary>Contains the state of a Simulation at any given time</summary>
    public class Grid
    {
        /// <summary>The current state for each cell</summary>
        public bool[,] state { get; private set; }
        /// <summary>The current number of living neighbors for each cell</summary>
        public int[,] livingNeighbors { get; private set; }

        /// <summary>The number of rows (in cells)</summary>
        public readonly int rowSize;
        /// <summary>The number of columns (in cells)</summary>
        public readonly int colSize;

        /// <summary>Create a new Grid instance from an initial state</summary>
        public Grid(bool[,] initialState)
        {
            state = initialState;

            rowSize = state.GetLength(0);
            colSize = state.GetLength(1);

            livingNeighbors = new int[rowSize, colSize];
        }

        /// <summary>Invert the state of a specified cell</summary>
        public void InvertState(int row, int col)
        {
            state[row, col] = !state[row, col];
        }

        /// <summary>Set the state to the state of another specified Grid</summary>
        public void FlashFrom(Grid inputGrid)
        {
            state = inputGrid.state;
        }

        /// <summary>Updates the number of living neighbors for all cells</summary>
        public void UpdateLivingNeighborsArray()
        {
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    livingNeighbors[i, j] = CountLivingNeighbors(i, j);
                }
            }
        }

        /// <summary>Returns the number of living neighbors for a specified cell</summary>
        public int CountLivingNeighbors(int row, int col)
        {
            int aliveCount = 0;

            for (int rowShift = -1; rowShift <= 1; rowShift++)
            {
                for (int colShift = -1; colShift <=1; colShift++)
                {
                    if (rowShift == 0 && colShift == 0) { continue; }
                    else
                    {
                        var rowMod = GridIndexHandler.Shift(row, rowShift, rowSize);
                        var colMod = GridIndexHandler.Shift(col, colShift, colSize);

                        bool isAlive = state[rowMod, colMod];
                        if (isAlive) { aliveCount += 1; }
                    }
                }
            }

            return aliveCount;
        }
    }
}