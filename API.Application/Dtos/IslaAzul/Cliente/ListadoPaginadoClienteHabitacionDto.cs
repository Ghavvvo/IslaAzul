using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;
using Newtonsoft.Json;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ListadoPaginadoClienteHabitacionDto 
    {   
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public List<DetallesHabitacionDto> Habitaciones { get; set; } = new();
     
    }
    
}
