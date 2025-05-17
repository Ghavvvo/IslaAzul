using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class HabitacionDto : EntidadBaseDto
    {
        public String Numero { get; set; } = string.Empty;
   
        public bool EstaFueraDeServicio { get; set; }
    
        public List<Reserva> Reservas { get; set; } = new();
    
        public List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();
    }
}

