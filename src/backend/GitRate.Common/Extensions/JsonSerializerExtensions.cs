using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            DefaultJsonOptions.Converters.Add(new JsonStringEnumMemberConverter());
        }

        /// <summary>
        /// Serializes obj to string
        /// </summary>
        public static string Serialize(this object obj, JsonSerializerOptions? jsonSerializerOptions = null)
        {
           return JsonSerializer.Serialize(obj, jsonSerializerOptions ?? DefaultJsonOptions);
        }

        /// <summary>
        /// Deserializes json string into object
        /// </summary>
        public static TModel Deserialize<TModel>(this string json, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<TModel>(json, jsonSerializerOptions ?? DefaultJsonOptions);
        }

        /// <summary>
        /// Десериализуется тело <see cref="HttpContent"/>
        /// </summary>
        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var stremContent = await httpContent.ReadAsStreamAsync();

            return await stremContent.ReadAsAsync<T>();
        }

        /// <summary>
        /// Десериализует поток данных в <typeparamref name="T"/>
        /// </summary>
        public static async Task<T> ReadAsAsync<T>(this Stream content)
        {
            return await JsonSerializer.DeserializeAsync<T>(content, DefaultJsonOptions);
        }
    }
}