using Data;
using Enums;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters
{
	public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
	{
        private ILogsData LogsData { get; }
        public int Order => int.MaxValue - 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logsData">Injected logs data layer</param>
		public HttpResponseExceptionFilter(ILogsData logsData)
		{
            LogsData = logsData;
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
                Log log = new()
                {
                    Description = context.Exception.Message,
                    Type = LogType.ERROR,
                    CreationDate = DateTime.Now
                };

                LogsData.Create(log);

                context.Result = new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}

