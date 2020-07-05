namespace GitRate.Web.Common.Contracts.Exception
{
    /// <summary>
    /// Represents single exception occured during web request
    /// </summary>
    public class ExceptionErrorContract
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Type of occured error
        /// </summary>
        public string Type { get; set; }
    }
}