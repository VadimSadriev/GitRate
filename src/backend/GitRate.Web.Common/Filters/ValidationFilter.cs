using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GitRate.Web.Common.Filters
{
    /// <summary>
    /// Validation filter for web request models
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var failures = new List<ValidationFailure>();

                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToArray();

                foreach (var error in errors)
                {
                    foreach (var modelError in error.Value.Errors)
                    {
                        failures.Add(new ValidationFailure(error.Key, modelError.ErrorMessage));
                    }
                }
                
                throw new ValidationException(failures);
            }

            await next();
        }
    }
}