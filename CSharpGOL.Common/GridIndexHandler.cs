using System;

namespace CSharpGOL.Common
{
    public static class GridIndexHandler
    {
        public static int Shift(int source, int shift, int dimensionSize)
        {
            return (source + shift + dimensionSize) % dimensionSize;
        }
    }
}
