using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class LogResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogResultFilter (APÓS) OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Filtro de Resource LogResultFilter (ANTES) OnResultExecuting");
        }
    }
}
