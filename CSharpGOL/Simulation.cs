namespace CSharpGOL
{
    /// <summary>
    /// A new Game of Life instance
    /// </summary>
    public class Simulation
    {
        /// <summary>The number of rows for the Grid (in cells)</summary>
        public readonly int rowSize;
        /// <summary>The number of columns for the Grid (in cells)</summary>
        public readonly int colSize;

        /// <summary>The Grid state for the previous generation</summary>
        public Grid previousGrid;
        /// <summary>The Grid state for the currrent generation</summary>
        public Grid currentGrid;

        /// <summary>The generation number for the current Grid state</summary>
        public int generation { get; private set; } = 0;

        /// <summary>
        /// Create a new Simulation from a random seed given row and column sizes
        /// </summary>
        public Simulation(int rowSize, int colSize)
        {
            this.rowSize = rowSize;
            this.colSize = colSize;

            var seed = SeedGenerator.New(rowSize, colSize);

            this.previousGrid = new Grid(seed);
            this.currentGrid = new Grid(seed);
        }

        /// <summary>
        /// Create a new Simulation from a specified seed
        /// </summary>
        public Simulation(bool[,] initialState)
        {
            this.previousGrid = new Grid(initialState);
            this.currentGrid = new Grid(initialState);    
        }

        /// <summary>
        /// Update the Grid to the next generation based on the rules
        /// </summary>
        public void NextGeneration()
        {
            previousGrid.FlashFrom(currentGrid);
            currentGrid.UpdateLivingNeighborsArray();

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    bool cellState = currentGrid.state[i, j];
                    int neighborCount = currentGrid.livingNeighbors[i, j];

                    if (cellState)
                    {
                        if (neighborCount < 2 || neighborCount > 3)
                            currentGrid.InvertState(i, j);
                    }
                    else
                    {
                        if (neighborCount == 3)
                            currentGrid.InvertState(i, j);
                    }
                }
            }

            generation += 1;
        }
    }
}