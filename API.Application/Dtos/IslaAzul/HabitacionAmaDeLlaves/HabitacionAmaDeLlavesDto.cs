using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class HabitacionAmaDeLlavesDto : EntidadBaseDto
    {
        public Guid HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; } = null!;
        public Guid AmaDeLlavesId { get; set; }
        public AmaDeLlaves AmaDeLlaves { get; set; } = null!;
    }
}

