namespace GitRate.Common.Exceptions
{
    /// <summary>
    /// Constants for exception types
    /// </summary>
    public class ExceptionTypes
    {
        /// <summary>
        /// Error occured 
        /// </summary>
        public const string DOMAIN_ERROR = "DOMAIN_ERROR";
        
        /// <summary>
        /// Error occured during business logic
        /// </summary>
        public const string APPLICATION_ERROR = "APPLICATION_ERROR";

        /// <summary>
        /// Error occured during validation
        /// </summary>
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";

        /// <summary>
        /// Error occured if requested entity not found
        /// </summary>
        public const string ENTITY_NOT_FOUND_ERROR = "ENTITY_NOT_FOUND_ERROR";
    }
}