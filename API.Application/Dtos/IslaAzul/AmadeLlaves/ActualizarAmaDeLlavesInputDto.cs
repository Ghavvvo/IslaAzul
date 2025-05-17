using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;
        
namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ActualizarAmaDeLlavesInputDto : AmaDeLlavesDto
    {
        [JsonIgnore]
        public new List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();
    
    }
}
