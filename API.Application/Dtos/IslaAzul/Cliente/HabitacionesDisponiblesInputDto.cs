using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class HabitacionesDisponiblesInputDto : ConfiguracionListadoPaginadoDto
    {
        public DateTime? FechaInicial { get; set; }
        
        public DateTime? FechaFinal  { get; set; }
    }
}
