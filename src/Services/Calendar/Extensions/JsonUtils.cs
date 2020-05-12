using System.Linq;
using Newtonsoft.Json;
using NJsonSchema;

namespace Calendar.Extensions
{
    public static class JsonUtils
    {
        public static bool TryParse<T>(string jsonData, out T result) where T : new()
        {
            var schema = JsonSchema.FromType<T>();
            bool isValid = !schema.Validate(jsonData).Any();
            result = isValid ? JsonConvert.DeserializeObject<T>(jsonData) : default;
            return isValid;
        }
    }
}
