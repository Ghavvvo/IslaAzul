using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class CrearHabitacionInputDto : HabitacionDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
        [JsonIgnore]
        public new List<Reserva> Reservas { get; set; } = new();
        
        [JsonIgnore]
        public new List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();

       
    }
}

