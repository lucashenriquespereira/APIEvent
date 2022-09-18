using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface ICityEventRepository
    {
        List<CityEvent> SelectEvents();
        List<CityEvent> SelectEventsByTitle(string title);
        List<CityEvent> SelectEventsByDateAndLocal(DateTime dateHourEvent, string local);
        List<CityEvent> SelectEventsByPriceAndDate(decimal low, decimal high, DateTime dateHourEvent);
        bool InsertCityEvent(CityEvent cityEvent);
        bool DeleteCityEvent(long id);
        bool UpdateCityEvent(CityEvent cityEvent);
    }
}
