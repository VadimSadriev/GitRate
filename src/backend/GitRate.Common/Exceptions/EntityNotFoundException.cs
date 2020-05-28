using System;

namespace GitRate.Common.Exceptions
{
    /// <summary>
    /// Thrown if requested entity not found
    /// </summary>
    public class EntityNotFoundException : AppException
    {
        /// <summary>
        /// Thrown if requested entity not found
        /// </summary>
        public EntityNotFoundException() { }

        /// <summary>
        /// Thrown if requested entity not found
        /// </summary>
        public EntityNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Thrown if requested entity not found
        /// </summary>
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Exception Type
        /// </summary>
        public override string Type => ExceptionTypes.ENTITY_NOT_FOUND_ERROR;
    }
}