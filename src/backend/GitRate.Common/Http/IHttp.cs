using System.Net.Http;
using System.Threading.Tasks;

namespace GitRate.Common.Http
{
    /// <summary>
    /// Http client for making http requests between applications 
    /// </summary>
    public interface IHttp
    {
        /// <summary>
        /// Defines uri to make request to
        /// </summary>
        /// <param name="uri">Source uri</param>
        IHttp For(string uri);

        /// <summary>
        /// Writes object to request body
        /// </summary>
        /// <typeparam name="TBody">Object type to write to body</typeparam>
        /// <param name="body">Actual object</param>
        IHttp WithBody<TBody>(TBody body) where TBody : class;

        /// <summary>
        /// Writes files to http form
        /// </summary>
        /// <param name="files">Collection of files to write to request</param>
        IHttp WithFiles(params FileRequest[] files);

        /// <summary>
        /// Adds object as query params
        /// </summary>
        /// <typeparam name="TQueryModel">Type of object to write to</typeparam>
        /// <param name="queryModel">Actual object to write to query</param>
        IHttp WithQueryParams<TQueryModel>(TQueryModel queryModel) where TQueryModel : class;

        /// <summary>
        /// Writes param to query
        /// </summary>
        /// <param name="key">Key of param</param>
        /// <param name="value">param value</param>
        IHttp WithQueryParam(string key, string value);

        /// <summary>
        /// Sends http request and returns <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="method">Http method</param>
        /// <param name="uri">Request uri</param>
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri);
    }
}
