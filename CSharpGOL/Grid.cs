namespace CSharpGOL
{
    public class Grid
    {
        public bool[,] state { get; private set; }
        
        public int rowSize
        {
            get
            {
                return state.GetLength(0);
            }
        }
        
        public int colSize
        {
            get
            {
                return state.GetLength(1);
            }
        }

        public Grid(bool[,] initialState)
        {
            state = initialState;
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