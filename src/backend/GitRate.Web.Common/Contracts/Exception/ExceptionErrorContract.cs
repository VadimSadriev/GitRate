namespace GitRate.Web.Common.Contracts.Exception
{
    /// <summary>
    /// Represents single exception occured during web request
    /// </summary>
    public class ExceptionErrorContract
    {
        public string Message { get; set; }
        
        public string Type { get; set; }
    }
}