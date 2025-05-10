using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ClienteDto : EntidadBaseDto
    {
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public required string Ci { get; set; } 
        public required bool EsVip { get; set; } 
        public required string Telefono { get; set; }
        public List<Reserva> Reservas { get; set; } = new();
    }
}

