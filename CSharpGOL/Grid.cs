namespace CSharpGOL
{
    public class Grid
    {
        public readonly int rowSize;
        public readonly int colSize;

        public bool[,] state; 

        public Grid(bool[,] initialState)
        {
            state = initialState;
        }

        public Grid(int rowSize, int colSize)
        {
            state = SeedGenerator.Create(rowSize, colSize);
        }

        public void InvertState(int rowIndex, int colIndex)
        {
            state[rowIndex, colIndex] = !state[rowIndex, colIndex];
        }

        public void FlashFrom(Grid inputGrid)
        {
            state = inputGrid.state;
        }
    }
}