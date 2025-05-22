using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class FiltrarConfigurarListadoPaginadoClienteHabitacionDto : ConfiguracionListadoPaginadoDto
    {
        public DateTime? Fecha { get; set; }
    }
}
