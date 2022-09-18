using APIEvent.Core.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using APIEvent.Core.Service;

namespace APIEvent.Filters
{
    public class ReservationShouldExistActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _eventReservationService;
        public ReservationShouldExistActionFilter(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            long idEventReservation = (long)context.ActionArguments["id"];

            if (_eventReservationService.SelectReservationById(idEventReservation) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}


