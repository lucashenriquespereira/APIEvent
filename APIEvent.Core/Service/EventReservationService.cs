using APIEvent.Core.Interface;
using APIEvent.Core.Model;

namespace APIEvent.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> SelectReservation()
        {
            return _eventReservationRepository.SelectReservation();
        }

        public List<EventReservation> SelectReservationByPersonNameAndTitle(string personName, string title)
        {
            return _eventReservationRepository.SelectReservationByPersonNameAndTitle(personName, title);
        }

        public bool InsertEventReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertEventReservation(eventReservation);
        }
        public bool UpdateEventReservation(long reservationId, long quantity)
        {

            return _eventReservationRepository.UpdateEventReservation(reservationId, quantity);
        }
        public bool DeleteEventReservation(long idReservation)
        {
            return _eventReservationRepository.DeleteEventReservation(idReservation);
        }
    }
}
