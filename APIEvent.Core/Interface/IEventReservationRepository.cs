using APIEvent.Core.Model;

namespace APIEvent.Core.Interface
{
    public interface IEventReservationRepository
    {
        List<EventReservation> SelectReservation();
        List<EventReservation> SelectReservationByPersonNameAndTitle(string personName, string title);
        bool InsertEventReservation(EventReservation eventReservation);
        bool UpdateEventReservation(long idReservation, long quantity);
        bool DeleteEventReservation(long idReservation);
    }
}
