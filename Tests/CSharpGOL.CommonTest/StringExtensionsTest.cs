using CSharpGOL.Common;
using System.Linq;
using System;
using Xunit;

namespace CSharpGOL.CommonTest
{
    public class StringExtensionsTest
    {
        [Fact]
        public void PadCenterReturnsOriginaIfTotalWidthNotGreaterThanInputLength()
        {
            // Arrange
            var originalString = String.Concat(
                Enumerable.Repeat('A', 10));
            var totalWidth = originalString.Length - 5;

            // Act
            var actualString = originalString.PadCenter(totalWidth);

            // Assert
            Assert.Equal(originalString, actualString);
        }

        [Fact]
        public void PadCenterReturnStringIsLengthOfTotalWidthIfGreaterThanInputStringLength()
        {
            // Arrange
            var originalString = String.Concat(
                Enumerable.Repeat('A', 10));
            var totalWidth = originalString.Length + 5;

            // Act
            var actualString = originalString.PadCenter(totalWidth);

            // Assert
            Assert.Equal(totalWidth, actualString.Length);
        }

        [Fact]
        public void PadCenterPadsWithSpacesIfNoCharacterIsSpecified()
        {
            // Arrange
            var originalString = String.Concat(
                Enumerable.Repeat('A', 10));
            var totalWidth = originalString.Length + 5;

            var expectedPaddingChar = ' ';

            // Act
            var actualString = originalString.PadCenter(totalWidth);

            // Assert
            Assert.Contains(expectedPaddingChar, actualString);
        }

        [Fact]
        public void PadCenterPadsWithSpecifiedCharacter()
        {
            // Arrange
            var originalString = String.Concat(
                Enumerable.Repeat('A', 10));
            var totalWidth = originalString.Length + 5;
            
            var expectedPaddingChar = '\u0023';

            // Act
            var actualString = originalString.PadCenter(totalWidth, expectedPaddingChar);

            // Assert
            Assert.Contains(expectedPaddingChar, actualString);
        }

        [Fact]
        public void PadCenterPutsRemainderForOddNumbersOfPaddingCharactersToRightSide()
        {
            // Arrange
            var originalString = String.Concat(
                Enumerable.Repeat('A', 10));
            var totalWidth = originalString.Length + 5;
            
            var paddingCharacter = '\u0023';

            var expectedLeftPadding = String.Concat(
                Enumerable.Repeat(paddingCharacter, 2));
            var expectedRightPadding = String.Concat(
                Enumerable.Repeat(paddingCharacter, 3));

            // Act
            var actualString = originalString.PadCenter(totalWidth, paddingCharacter);
            
            var actualLeftPadding = actualString.Substring(0, expectedLeftPadding.Length);
            var actualRightPadding = actualString.Substring(
                originalString.Length + expectedLeftPadding.Length);
            
            // Assert
            Assert.Equal(expectedLeftPadding, actualLeftPadding);
            Assert.Equal(expectedRightPadding, actualRightPadding);

        }
    }
}