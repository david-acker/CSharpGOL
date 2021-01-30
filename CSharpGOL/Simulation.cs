namespace CSharpGOL
{
    public class Simulation
    {
        public readonly int rowSize;
        public readonly int colSize;

        public Grid previousGrid;
        public Grid currentGrid;

        public int generation { get; private set; } = 0;

        public Simulation(int rowSize, int colSize)
        {
            this.rowSize = rowSize;
            this.colSize = colSize;

            var seed = SeedGenerator.New(rowSize, colSize);

            this.previousGrid = new Grid(seed);
            this.currentGrid = new Grid(seed);
        }

        public Simulation(bool[,] initialState)
        {
            this.previousGrid = new Grid(initialState);
            this.currentGrid = new Grid(initialState);    
        }

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