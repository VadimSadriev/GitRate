using System.Collections.Generic;

namespace GitRate.Web.Common.Contracts.Exception
{
    /// <summary>
    /// Contract for all kind of errors occured during web request
    /// </summary>
    public class ExceptionContract
    {
        public IEnumerable<ExceptionErrorContract> Errors { get; set; }
    }
}