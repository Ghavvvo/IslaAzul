using API.Data.DbContexts;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Repositorios.Seguridad
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
