namespace CSharpGOL.Core.Extensions
{
    internal static class StringExtensions
    {
        public static string PadCenter(this string str, int totalWidth, char paddingCharacter = ' ')
        {
            int paddingCharacterCount = totalWidth - str.Length;
            if (paddingCharacterCount < 1)
            {
                return str;
            }

            int leftPaddingCharacterCount = (paddingCharacterCount / 2) + str.Length;

            return str.PadLeft(leftPaddingCharacterCount, paddingCharacter)
                .PadRight(totalWidth, paddingCharacter);
        }
    }
}
