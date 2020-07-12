using System;
using System.Net;

namespace GitRate.Common.Http.Exceptions
{
    /// <summary>
    /// Exception thrown if http request failed
    /// </summary>
    public class HttpException : Exception
    {
        /// <summary>
        /// Exception thrown if http request failed
        /// </summary>
        public HttpException(string message) : base(message)
        {

        }

        /// <summary>
        /// Exception thrown if http request failed
        /// </summary>
        public HttpException(string message, HttpStatusCode statusCode, string requestUri, string responseBody) : base(message)
        {
            StatusCode = statusCode;
            RequestUri = requestUri;
            ResponseBody = responseBody;
        }

        /// <summary>
        /// Exception thrown if http request failed
        /// </summary>
        public HttpException(string message, Exception innerException, HttpStatusCode statusCode, string requestUri, string responseBody) : base(message, innerException)
        {
            StatusCode = statusCode;
            RequestUri = requestUri;
            ResponseBody = responseBody;
        }

        /// <summary>
        /// Returned status code
        /// </summary>
        public HttpStatusCode? StatusCode { get; }

        /// <summary>
        /// Request uri
        /// </summary>
        public string? RequestUri { get; }

        /// <summary>
        /// Returned body
        /// </summary>
        public string? ResponseBody { get; }
    }
}
