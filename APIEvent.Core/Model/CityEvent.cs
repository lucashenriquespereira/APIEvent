using ProjetoFinalAPI.Core.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEvent.Core.Model
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        
        [Required (ErrorMessage = "O nome do evento é necessário para fazer o cadastro")]
        public string Title { get; set; }
        
        public string? Description { get; set; }
        
        [Required (ErrorMessage = "O data quando o evento acontecerá é necessário para o cadastro")]
        public DateTime DateHourEvent { get; set; }
        
        [Required (ErrorMessage = "O local do evento é necessário para o cadastro")]
        public string Local { get; set; }
        
        public string? Address { get; set; }
 
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "O status do evento é necessário para o cadastro")]
        public bool Status { get; set; } = true;
    }
}