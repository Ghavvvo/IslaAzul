using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Domain.Validators.Seguridad;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Interfaces.Seguridad
{
    public interface IReservaService : IBaseService<Reserva, ReservaValidator>
    {
        Task<EntityEntry<Reserva>> ActualizarRegistrarLlegadaReserva(Reserva reserva);

        Task<EntityEntry<Reserva>> CancelarReserva(Reserva entity);
    }
}