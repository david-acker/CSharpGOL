using CSharpGOL.Common;

namespace CSharpGOL
{
    public class Grid
    {
        public bool[,] state { get; private set; }
        public int[,] livingNeighbors { get; private set; }
        
        public readonly int rowSize;
        public readonly int colSize;

        public Grid(bool[,] initialState)
        {
            state = initialState;

            rowSize = state.GetLength(0);
            colSize = state.GetLength(1);

            livingNeighbors = new int[rowSize, colSize];
        }

        public void InvertState(int row, int col)
        {
            state[row, col] = !state[row, col];
        }

        public void FlashFrom(Grid inputGrid)
        {
            state = inputGrid.state;
        }

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