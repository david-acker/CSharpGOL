namespace CSharpGOL.Core.Helpers;

internal static class GridHelper
{
    public static int GetWrapAroundIndex(int index, int shift, int dimensionSize)
    {
        if (dimensionSize < 1)
        {
            throw new ArgumentException("The dimension size must be greater than zero.");
        }

        if (index < 0 || index >= dimensionSize)
        {
            throw new ArgumentException("The provided index falls outside of the valid range.");
        }

        return (index + shift + dimensionSize) % dimensionSize;
    }
}
