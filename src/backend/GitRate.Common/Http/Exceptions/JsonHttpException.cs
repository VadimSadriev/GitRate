using System;
using System.Net;

namespace GitRate.Common.Http.Exceptions
{
    /// <summary> Thrown during json http interaction </summary>
    public class JsonHttpException : HttpException
    {
        /// <summary> Thrown during json http interaction </summary>
        public JsonHttpException(string message) : base(message)
        {

        }

        /// <summary> Thrown during json http interaction </summary>
        public JsonHttpException(string message, HttpStatusCode statusCode, string requestUri, string responseBody)
            : base(message, statusCode, requestUri, responseBody)
        {
        }

        /// <summary> Thrown during json http interaction </summary>
        public JsonHttpException(string message, Exception innerException, HttpStatusCode statusCode, string requestUri, string responseBody)
            : base(message, innerException, statusCode, requestUri, responseBody)
        {
        }
    }
}
