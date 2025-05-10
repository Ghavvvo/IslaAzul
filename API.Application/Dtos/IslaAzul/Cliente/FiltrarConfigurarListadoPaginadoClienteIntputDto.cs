using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class FiltrarConfigurarListadoPaginadoClienteIntputDto : ConfiguracionListadoPaginadoDto
    {
        public string Nombre { get; set; } = string.Empty;
        
        public string Apellidos { get; set; } = string.Empty;
        
        public string Ci { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

    }
}
