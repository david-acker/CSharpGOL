namespace CSharpGOL
{
    public class Grid
    {
        public bool[,] state { get; private set; }
        
        public readonly int rowSize;
        public readonly int colSize;

        public Grid(bool[,] initialState)
        {
            state = initialState;

            this.rowSize = state.GetLength(0);
            this.colSize = state.GetLength(1);
        }

        public void InvertState(int row, int col)
        {
            state[row, col] = !state[row, col];
        }

        public void FlashFrom(Grid inputGrid)
        {
            state = inputGrid.state;
        }
    }
}