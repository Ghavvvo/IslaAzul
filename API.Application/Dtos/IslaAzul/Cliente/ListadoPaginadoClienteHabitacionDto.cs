using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;
using Newtonsoft.Json;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ListadoPaginadoClienteHabitacionDto 
    {   
        public required string Cliente { get; set; }
      
        public HabitacionesOcupadasDto  Habitacion { get; set; } = new();
        
        public DateTime FechaEntrada { get; set; }
        
        public DateTime FechaSalida { get; set; }
        
        
     
    }
    
}
