using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public IEventReservationRepository _eventReservationRepository;
        public CityEventService(ICityEventRepository cityEventRepository, IEventReservationRepository eventReservationRepository)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationRepository = eventReservationRepository;
        }

        public List<CityEvent> SelectEvents()
        {
            return _cityEventRepository.SelectEvents();
        }

        public List<CityEvent> SelectEventsByTitle(string title)
        {
            return _cityEventRepository.SelectEventsByTitle(title);
        }
        public List<CityEvent> SelectEventsByDateAndLocal(DateTime dateHourEvent, string local)
        {
            return _cityEventRepository.SelectEventsByDateAndLocal(dateHourEvent, local);
        }
        public List<CityEvent> SelectEventsByPriceAndDate(decimal low, decimal high, DateTime dateHourEvent)
        {
            return _cityEventRepository.SelectEventsByPriceAndDate(low, high, dateHourEvent);
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertCityEvent(cityEvent);
        }
        public bool DeleteCityEvent(long idEvent)
        {
            var eventReservationList = _eventReservationRepository.SelectReservation().ToList();
            if (!eventReservationList.Any(x => x.IdEvent == idEvent))
            { 
                return _cityEventRepository.DeleteCityEvent(idEvent);
            }
            var cityEventList = _cityEventRepository.SelectEvents().ToList();
            var active = cityEventList.FirstOrDefault(x => x.IdEvent == idEvent);
            active.Status = false;
            return _cityEventRepository.UpdateCityEvent(active);
        }
        public bool UpdateCityEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.UpdateCityEvent(cityEvent);
        }
    }
}