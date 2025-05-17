using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ActualizarHabitacionAmaDeLlavesInputDto : HabitacionAmaDeLlavesDto
    {
        [JsonIgnore]
        public new List<Reserva> Reservas { get; set; } = new();
    
    }
}
