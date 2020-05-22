using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitRate.Common.Extensions
{
    public static class JsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        
        static JsonSerializerExtensions()
        {
            DefaultJsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static string Serialize(this object obj)
        {
           return JsonSerializer.Serialize(obj, DefaultJsonOptions);
        }
    }
}