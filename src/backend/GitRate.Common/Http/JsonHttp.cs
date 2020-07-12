using GitRate.Common.Extensions;
using GitRate.Common.Http.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitRate.Common.Http
{
    /// <summary>
    /// Http client to interact with external services via json
    /// </summary>
    public class JsonHttp : Http
    {
        /// <summary>
        /// Http client to interact with external services via json
        /// </summary>
        public JsonHttp(HttpClient httpClient, ILogger<JsonHttp> logger) : base(httpClient, logger)
        {
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override HttpContent BuildRequestBody<TBody>(TBody body)
        {
            if (body == null)
                return null;

            return new StringContent(body.Serialize(), Encoding.UTF8, HttpConstants.MimeTypes.Json);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override async Task<TResponseBody> BuildResponseBody<TResponseBody>(HttpResponseMessage response)
        {
            if (response.Content == null)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();

            if (response.Content.Headers.ContentType.MediaType != HttpConstants.MimeTypes.Json)
                throw new JsonHttpException("Response is not in json type", response.StatusCode,
                    response.RequestMessage.RequestUri.ToString(), responseString);

            return responseString.Deserialize<TResponseBody>();
        }
    }
}
