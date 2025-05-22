using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class CrearHabitacionAmaDeLlavesInputDto : HabitacionAmaDeLlavesDto
    {   
        [JsonIgnore]
        new public Guid Id { get; set; }
        
        [JsonIgnore]
        public new AmaDeLlaves AmaDeLlaves { get; set; } = null!;

        [JsonIgnore]
        public new Habitacion Habitacion { get; set; } = null!;
    }
}

