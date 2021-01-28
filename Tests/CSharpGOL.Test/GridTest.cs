using CSharpGOL;
using System.Drawing;
using System;
using Xunit;
using System.Collections.Generic;

namespace CSharpGOL.Test
{
    public class GridTest
    {
        [Fact]
        public void GridStateInstantiatedFromInputBooleanArray()
        {
            // Arrange
            var initialState = new bool[3, 3]
            {
                { true,  false, true  },
                { false, true,  false },
                { true,  false, true  }
            };

            // Act
            var grid = new Grid(initialState);

            // Assert
            Assert.Equal(initialState, grid.state);
        }

        [Fact]
        public void CellStateInGridInverted()
        {
            // Arrange
            var initialState = new bool[3, 3]
            {
                { true,  false, true  },
                { false, true,  false },
                { true,  false, true  }
            };

            var grid = new Grid(initialState);

            var expectedState = new bool[3, 3]
            {
                { false, false, true  },
                { false, false, false },
                { true,  false, false }
            };

            var targetCells = new List<Point>()
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2)
            };

            // Act
            foreach (Point cell in targetCells)
            {
                grid.InvertState(cell.X, cell.Y);
            }

            // Assert
            Assert.Equal(expectedState, grid.state);
        }

        [Fact]
        public void GridStateFlashedFromSourceGrid()
        {
            // Arrange
            var initialState = new bool[3, 3]
            {
                { true,  false, true  },
                { false, true,  false },
                { true,  false, true  }
            };

            var expectedState = new bool[3, 3]
            {
                { false, false, true  },
                { false, false, false },
                { true,  false, false }
            };

            var mainGrid = new Grid(initialState);
            var sourceGrid = new Grid(expectedState);

            // Act
            mainGrid.FlashFrom(sourceGrid);

            // Assert
            Assert.Equal(expectedState, mainGrid.state);
        }

        [Fact]
        public void GridReturnsCorrectRowSize()
        {
            // Arrange
            var initialState = new bool[3, 3]
            {
                { true,  false, true  },
                { false, true,  false },
                { true,  false, true  }
            };

            var grid = new Grid(initialState);


            // Act
            var gridRowSize = grid.rowSize;

            // Assert
            Assert.Equal(initialState.GetLength(0), gridRowSize);
        }

        [Fact]
        public void GridReturnsCorrectColumnSize()
        {
            // Arrange
            var initialState = new bool[3, 3]
            {
                { true,  false, true  },
                { false, true,  false },
                { true,  false, true  }
            };

            var grid = new Grid(initialState);

            // Act
            var gridColSize = grid.colSize;

            // Assert
            Assert.Equal(initialState.GetLength(1), gridColSize);
        }

    }
}
