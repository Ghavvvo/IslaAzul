using System.Text.Json.Serialization;
using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class CambiarDeHabitacionInputDto : EntidadBaseDto
    {
        
        public Guid HabitacionId { get; set; }

    }
}
