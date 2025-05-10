using API.Data.DbContexts;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Repositorios.Seguridad
{
    public class HabitacionRepository : BaseRepository<Habitacion>, IHabitacionRepository
    {
        public HabitacionRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
