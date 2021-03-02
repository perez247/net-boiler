using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    /// <summary>
    /// For filter for errors
    /// </summary>
    public class CustomActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Method that gets invoked
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new Exception("Error");
            }

            base.OnActionExecuting(context);
        }

    }
}