using System.Collections.Generic;
using System.Drawing;

namespace CSharpGOL
{
    public class Simulation
    {
        public readonly int rowSize;
        public readonly int colSize;

        public Grid previousGrid { get; private set; }
        public Grid currentGrid { get; private set; }

        private readonly List<Point>[,] neighborCoordinateArray;
        private int[,] livingNeighborsCountArray; 

        public int generation { get; private set; } = 0;

        public Simulation(int rowSize, int colSize)
        {
            this.rowSize = rowSize;
            this.colSize = colSize;

            this.previousGrid = new Grid(rowSize, colSize);
            this.currentGrid = new Grid(rowSize, colSize);

            neighborCoordinateArray = CreateNeighborCoordinateArray();
            livingNeighborsCountArray = new int[rowSize, colSize];
        }

        public Simulation(bool[,] initialState)
        {
            this.previousGrid = new Grid(initialState);
            this.currentGrid = new Grid(initialState);
            neighborCoordinateArray = CreateNeighborCoordinateArray();
            livingNeighborsCountArray = new int[rowSize, colSize];        
        }

        public void NextGeneration()
        {
            previousGrid.FlashFrom(currentGrid);
            UpdateLivingNeighborsCountArray();

            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    bool cellState = currentGrid.state[row, col];
                    int neighborCount = livingNeighborsCountArray[row, col];

                    if (cellState)
                    {
                        if (neighborCount < 2 || neighborCount > 3)
                            currentGrid.InvertState(row, col);
                    }
                    else
                    {
                        if (neighborCount == 3)
                            currentGrid.InvertState(row, col);
                    } 
                }
            }
            generation += 1;
        }

        public void UpdateLivingNeighborsCountArray()
        {
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    livingNeighborsCountArray[row, col] = CountLivingNeighbors(row, col);
                }
            }
        }

        public int CountLivingNeighbors(int row, int col)
        {
            List<Point> cellNeighbors = neighborCoordinateArray[row, col]; 

            int neighborsAlive = 0;
            foreach (Point neighbor in cellNeighbors)
            {
                bool isAlive = currentGrid.state[neighbor.X, neighbor.Y];
                if (isAlive) { neighborsAlive += 1; }
            }
            return neighborsAlive;
        }

        public List<Point> GetNeighborCoordinates(int row, int col)
        {
            // Calculate row indicies for positive & negative shifts
            var rowShiftPositive = row + 1;
            if (rowShiftPositive == rowSize) { rowShiftPositive = 0; }

            var rowShiftNegative = row - 1;
            if (rowShiftNegative < 0) { rowShiftNegative = rowSize - 1; }

            // Calculate column indicies for positive & negative shifts
            var colShiftPositive = col + 1;
            if (colShiftPositive == colSize) { colShiftPositive = 0; }

            var colShiftNegative = col - 1;
            if (colShiftNegative < 0) { colShiftNegative = colSize - 1; }            

            // Create neighbor points where X is row and Y is column
            List<Point> neighborPointList = new List<Point>()
            {
                // Define points for vertical neighbors
                new Point(rowShiftPositive, col),
                new Point(rowShiftNegative, col),

                // Define points for horizontal neighbors
                new Point(row, colShiftPositive),
                new Point(row, colShiftNegative),
            
                // Define points for diagonal neighbors
                new Point(rowShiftPositive, colShiftPositive),
                new Point(rowShiftPositive, colShiftNegative),
                new Point(rowShiftNegative, colShiftPositive),
                new Point(rowShiftNegative, colShiftNegative), 
            };
            return neighborPointList;            
        }

        public List<Point>[,] CreateNeighborCoordinateArray()
        {
            var coordinateArray = new List<Point>[rowSize, colSize];
            
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    coordinateArray[row, col] = GetNeighborCoordinates(row, col);
                }
            }
            return coordinateArray;
        }
    }
}