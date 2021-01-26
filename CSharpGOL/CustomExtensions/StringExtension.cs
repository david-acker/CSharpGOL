namespace CustomExtensions
{
    public static class StringExtensions
    {
        public static string PadCenter(this string str, int totalWidth)
        {
            int charactersToPad = totalWidth - str.Length;
            if (charactersToPad > 0)
            {
                int leftPadSize = (charactersToPad / 2) + str.Length;
                return str.PadLeft(leftPadSize).PadRight(totalWidth);
            }
            else
            {
                return str;
            }
        }

        public static string PadCenter(this string str, int totalWidth, char paddingCharacter)
        {
            int charactersToPad = totalWidth - str.Length;
            if (charactersToPad > 0)
            {
                int leftPadSize = (charactersToPad / 2) + str.Length;
                return str.PadLeft(leftPadSize, paddingCharacter)
                          .PadRight(totalWidth, paddingCharacter);
            }
            else
            {
                return str;
            }
        }
    }
}