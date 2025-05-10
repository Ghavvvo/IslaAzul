using System.Text.Json.Serialization;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class CrearClienteInputDto : ClienteDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

        [JsonIgnore]
        public new List<Reserva> Reservas { get; set; } = new();
    }
}

