using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitRate.Common.Http
{
    /// <summary> Http client for making http requests between applications </summary>
    public interface IHttp
    {
        /// <summary> Defines uri to make request to </summary>
        /// <param name="uri">Source uri</param>
        IHttp For(string uri);

        /// <summary> Defines uri to make request to </summary>
        /// <param name="uri">Source uri</param>
        IHttp For(Uri uri);

        /// <summary> Writes object to request body </summary>
        /// <typeparam name="TBody">Object type to write to body</typeparam>
        /// <param name="body">Actual object</param>
        IHttp WithBody<TBody>(TBody body) where TBody : class;

        /// <summary> Writes files to http form </summary>
        /// <param name="files">Collection of files to write to request</param>
        IHttp WithFiles(params FileRequest[] files);

        /// <summary> Adds object as query params </summary>
        /// <typeparam name="TQueryModel">Type of object to write to</typeparam>
        /// <param name="queryModel">Actual object to write to query</param>
        IHttp WithQueryParams<TQueryModel>(TQueryModel queryModel) where TQueryModel : class;

        /// <summary> Writes params to query </summary>
        /// <param name="queryParams">List of KVP to write to request query</param>
        IHttp WithQueryParams(List<KeyValuePair<string, string>> queryParams);

        /// <summary> Sends post http request </summary>
        Task<TResponse> PostAsync<TResponse>() where TResponse : class;

        /// <summary> Sends get http request </summary>
        Task<TResult> GetAsync<TResult>() where TResult : class;

        /// <summary>
        /// Sends request and returns response as <typeparamref name="TResponse"/>
        /// </summary>
        Task<TResponse> SendAsync<TResponse>(HttpMethod method) where TResponse : class;

        /// <summary> Sends http request and returns <see cref="HttpResponseMessage"/> </summary>
        /// <param name="method">Http method</param>
        /// <param name="uri">Request uri</param>
        Task<HttpResponseMessage> SendAsync(HttpMethod method);
    }
}
