using System;

namespace CSharpGOL
{
    public static class SeedGenerator
    {
        public static bool[,] New(int rowSize, int colSize)
        {
            var rand = new Random();

            bool[,] seed = new bool[rowSize, colSize];
            FillSeedPattern(seed, 5, rand);

            return seed;
        }

        private static void FillSeedPattern(bool[,] seed, int density, Random rand)
        {
            for (int i = 0; i < seed.GetLength(0); i++)
            {
                for (int j = 0; j < seed.GetLength(1); j++)
                {
                    seed[i, j] = RandomCellState(density, rand);
                }
            }
        }

        private static bool RandomCellState(int density, Random rand)
        {
            int randInt = rand.Next(11);
            
            bool cellState = false;
            if (randInt < density) { cellState = true; }

            return cellState;
        }
    }
}