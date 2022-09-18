using APIEvent.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvent.Filters
{
    public class EventShouldExistActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public EventShouldExistActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            long idCityEvent = (long)context.ActionArguments["id"];

            if (_cityEventService.SelectEventById(idCityEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
