using System;

namespace GitRate.Common.Exceptions
{
    /// <summary>
    /// Base application exception
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Base application exception
        /// </summary>
        public AppException() { }

        /// <summary>
        /// Base application exception
        /// </summary>
        public AppException(string message) : base(message) { }

        /// <summary>
        /// Base application exception
        /// </summary>
        public AppException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Exception Type
        /// </summary>
        public virtual string Type => ExceptionTypes.APPLICATION_ERROR;
    }
}