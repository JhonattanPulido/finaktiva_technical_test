using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters
{
	public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
	{
        public int Order => int.MaxValue - 10;

		public HttpResponseExceptionFilter()
		{
		}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null)
            {
                context.Result = new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}

