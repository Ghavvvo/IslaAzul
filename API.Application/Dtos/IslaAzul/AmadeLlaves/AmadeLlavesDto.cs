using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class AmaDeLlavesDto : EntidadBaseDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Ci { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
        
        public List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();
       
    }
}

