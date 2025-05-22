using API.Application.Dtos.Comunes;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ClienteDto : EntidadBaseDto
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Ci { get; set; } 
        public bool EsVip { get; set; } 
        public  string Telefono { get; set; }
        public List<Reserva> Reservas { get; set; } = new();
    }
}

