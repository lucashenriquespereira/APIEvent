using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIEvent.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Unexpected Error",
                Detail = "An unexpected error occurred in the request",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Exception type {context.Exception.GetType().Name}, message {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch (context.Exception)
            {
                case ArgumentNullException:
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                    break;
                case DivideByZeroException:
                    problem.Status = StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }
        }
    }
}