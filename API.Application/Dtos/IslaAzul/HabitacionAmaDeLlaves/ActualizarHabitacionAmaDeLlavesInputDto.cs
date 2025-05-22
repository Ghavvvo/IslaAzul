using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ActualizarHabitacionAmaDeLlavesInputDto : HabitacionAmaDeLlavesDto
    {
     
        
        [JsonIgnore]
        public new AmaDeLlaves AmaDeLlaves { get; set; } = null!;

        [JsonIgnore]
        public new Habitacion Habitacion { get; set; } = null!;
        
    
    }
}
