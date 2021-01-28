using CSharpGOL.Common;
using System;
using Xunit;

namespace CSharpGOL.CommonTest
{
    public class GridIndexHandlerTest
    {
        [Fact]
        public void ZeroShiftReturnsOriginalIndex()
        {
            // Arrange
            int originalIndex = 4;
            int zeroShift = 0;
            int dimensionSize = 5;

            int expectedIndex = originalIndex;

            // Act
            var actualIndex = GridIndexHandler.Shift(
                originalIndex, zeroShift, dimensionSize);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void PositiveShiftIncreasesIndex()
        {
            // Arrange
            int originalIndex = 3;
            int positiveShift = 1;
            int dimensionSize = 5;

            int expectedIndex = 4;

            // Act
            var actualIndex = GridIndexHandler.Shift(
                originalIndex, positiveShift, dimensionSize);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void NegativeShiftDecreasesIndex()
        {
            // Arrange
            int originalIndex = 4;
            int negativeShift = -1;
            int dimensionSize = 5;

            int expectedIndex = 3;

            // Act
            var actualIndex = GridIndexHandler.Shift(
                originalIndex, negativeShift, dimensionSize);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void PostiveShiftWrapsAroundOnEdge()
        {
            // Arrange
            int originalIndex = 4;
            int positiveShift = 1;
            int dimensionSize = 5;

            int expectedIndex = 0;

            // Act
            int actualIndex = GridIndexHandler.Shift(
                originalIndex, positiveShift, dimensionSize);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void NegativeShiftWrapsAroundOnEdge()
        {
            // Arrange
            int originalIndex = 0;
            int negativeShift = -1;
            int dimensionSize = 5;

            int expectedIndex = 4;

            // Act
            int actualIndex = GridIndexHandler.Shift(
                originalIndex, negativeShift, dimensionSize);

            // Assert
            Assert.Equal(expectedIndex, actualIndex);
        }
    }
}
