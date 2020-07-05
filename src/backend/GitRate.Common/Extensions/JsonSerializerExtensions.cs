using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="JsonSerializer"/>
    /// </summary>
    public static class JsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Extensions for <see cref="JsonSerializer"/>
        /// </summary>
        static JsonSerializerExtensions()
        {
            DefaultJsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Serializes obj to string
        /// </summary>
        public static string Serialize(this object obj, JsonSerializerOptions? jsonSerializerOptions = null)
        {
           return JsonSerializer.Serialize(obj, jsonSerializerOptions ?? DefaultJsonOptions);
        }
    }
}