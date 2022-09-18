using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogActionFilter (APÓS) OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Filtro de Resource LogActionFilter (ANTES) OnActionExecuting");
        }
    }
}
