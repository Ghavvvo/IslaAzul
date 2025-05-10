using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Domain.Validators.Seguridad;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Interfaces.Seguridad
{
    public interface IReservaService : IBaseService<Reserva, ReservaValidator>
    {
        
    }
}