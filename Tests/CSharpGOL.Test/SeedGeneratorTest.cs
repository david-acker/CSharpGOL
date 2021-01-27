using System;
using Xunit;

namespace CSharpGOL.Test
{
    public class SeedGeneratorTest
    {
        [Fact]
        public void SeedIsABooleanArray()
        {
            // Arrange
            int rowSize = 5;
            int colSize = 5;

            // Act
            var seed = SeedGenerator.New(rowSize, colSize);

            // Assert
            Assert.IsType<bool[,]>(seed);
        }

        [Fact]
        public void SeedUsesTheSpecifiedRowSize()
        {
            // Arrange
            int firstRowSize = 5;
            int firstColSize = 5;

            int secondRowSize = 10;
            int secondColSize = 20;

            int thirdRowSize = 25;
            int thirdColSize = 15;

            // Act
            var firstSeed = SeedGenerator.New(firstRowSize, firstColSize);
            var secondSeed = SeedGenerator.New(secondRowSize, secondColSize);
            var thirdSeed = SeedGenerator.New(thirdRowSize, thirdColSize);

            // Assert
            Assert.Equal(firstRowSize, firstSeed.GetLength(0));
            Assert.Equal(secondRowSize, secondSeed.GetLength(0));
            Assert.Equal(thirdRowSize, thirdSeed.GetLength(0));                  
        }

        [Fact]
        public void SeedUsesTheSpecifiedColumnSize()
        {
            // Arrange
            int firstRowSize = 5;
            int firstColSize = 5;

            int secondRowSize = 10;
            int secondColSize = 20;

            int thirdRowSize = 25;
            int thirdColSize = 15;

            // Act
            var firstSeed = SeedGenerator.New(firstRowSize, firstColSize);
            var secondSeed = SeedGenerator.New(secondRowSize, secondColSize);
            var thirdSeed = SeedGenerator.New(thirdRowSize, thirdColSize);

            // Assert
            Assert.Equal(firstColSize, firstSeed.GetLength(1));
            Assert.Equal(secondColSize, secondSeed.GetLength(1));
            Assert.Equal(thirdColSize, thirdSeed.GetLength(1));            
        }

        [Fact]
        public void AllCellsInSeedHaveBooleanState()
        {
            // Arrange
            int rowSize = 5;
            int colSize = 5;

            // Act
            var seed = SeedGenerator.New(rowSize, colSize);

            // Assert
            for (int i = 0; i < seed.GetLength(0); i++)
            {
                for (int j = 0; j < seed.GetLength(1); j++)
                {
                    var cell = seed[i, j];

                    Assert.IsType<bool>(cell);
                }
            }
        }
    }
}
