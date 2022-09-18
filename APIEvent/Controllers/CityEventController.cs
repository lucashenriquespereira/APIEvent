using APIEvent.Core.Interface;
using APIEvent.Core.Model;
using APIEvent.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIEvent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]
    [Authorize]

    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/cityEvent/GetEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<CityEvent> GetEvent()
        {
            var cityEvent = _cityEventService.SelectEvents();
            if (cityEvent == null)
            {
                return NotFound();
            }
            return Ok(cityEvent);
        }

        [HttpGet("/cityEvent/GetEventByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventsByTitle(string title)
        {
            var cityEventList = _cityEventService.SelectEventsByTitle(title);
            if (!cityEventList.Any())
                return NotFound();
            return Ok(cityEventList);
        }

        [HttpGet("/cityEvent/GetEventByDateAndLocal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventsByDateAndLocal(DateTime dateHourEvent, string local)
        {
            var cityEventList = _cityEventService.SelectEventsByDateAndLocal(dateHourEvent, local);
            if (!cityEventList.Any())
                return NotFound();
            return Ok(cityEventList);
        }

        [HttpGet("/cityEvent/GetEventByPriceAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetEventsByPriceAndDate(decimal low, decimal high, DateTime dateHourEvent)
        {
            var cityEventList = _cityEventService.SelectEventsByPriceAndDate(low, high, dateHourEvent);
            if (!cityEventList.Any())
                return NotFound();
            return Ok(cityEventList);
        }

        [HttpPost("/cityEvent/Insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(LogActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> PostCityEvent(CityEvent cityEvent)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.InsertCityEvent(cityEvent))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostCityEvent), cityEvent);
        }

        [HttpPut("/cityEvent/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [TypeFilter(typeof(LogActionFilter))]
        [ServiceFilter(typeof(EventShouldExistActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCityEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.UpdateCityEvent(cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(EventShouldExistActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> DeleteCityEvent(long id)
        {
            if (!_cityEventService.DeleteCityEvent(id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
