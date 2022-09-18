using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class EventReservation
    {
        
        public long IdReservation { get; set; }
        
        [Required (ErrorMessage = "O id do evento precisa ser colodado")]
        public long IdEvent { get; set; }

        [Required (ErrorMessage ="O nome de quem fez a reserva é necessario para o cadastro")]
        public string PersonName { get; set; }

        [Required (ErrorMessage = "A quantidade de convites é necessária para o cadastro")]
        public long Quantity { get; set; }
    }
}
