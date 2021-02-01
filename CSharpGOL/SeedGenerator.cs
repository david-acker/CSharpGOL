using System;

namespace CSharpGOL
{
    /// <summary>Produces seeds for the initial state of a Grid.</summary>
    public static class SeedGenerator
    {
        /// <summary>Return new array with randomly set states.</summary>
        public static bool[,] New(int rowSize, int colSize)
        {
            var rand = new Random();
            var seed = new bool[rowSize, colSize];

            FillSeedPattern(seed, 5, rand);

            return seed;
        }

        /// <summary>Randomly set the states of all the cells in the array.</summary>
        private static void FillSeedPattern(bool[,] seed, int density, Random rand)
        {
            for (var i = 0; i < seed.GetLength(0); i++)
            {
                for (var j = 0; j < seed.GetLength(1); j++)
                {
                    seed[i, j] = RandomCellState(density, rand);
                }
            }
        }

        /// <summary>Return a random boolean representing a cell state.</summary>
        private static bool RandomCellState(int density, Random rand)
        {
            int randInt = rand.Next(11);
            
            bool cellState = false;
            if (randInt < density) { cellState = true; }

            return cellState;
        }
    }
}