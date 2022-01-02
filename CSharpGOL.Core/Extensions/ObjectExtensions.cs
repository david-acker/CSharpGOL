using Newtonsoft.Json;

namespace CSharpGOL.Core.Extensions
{
    internal static class ObjectExtensions
    {
        public static T? DeepCopy<T>(this T? input) where T : class
        {
            string serializedInput = JsonConvert.SerializeObject(input);
            return JsonConvert.DeserializeObject<T>(serializedInput);
        }
    }
}
