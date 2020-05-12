using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Calendar.Extensions
{
    public static class JsonUtils
    {
        public static bool TryParse<T>(string jsonData, out T result) where T : new()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema parsedSchema = generator.Generate(typeof(T));
            JObject jObject = JObject.Parse(jsonData);

            bool isValid = jObject.IsValid(parsedSchema);
            result = isValid ? JsonConvert.DeserializeObject<T>(jsonData) : default;
            return isValid;
        }
    }
}
