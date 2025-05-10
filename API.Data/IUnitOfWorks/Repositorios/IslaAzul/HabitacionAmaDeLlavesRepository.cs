using API.Data.DbContexts;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Repositorios.Seguridad
{
    public class HabitacionAmaDeLlavesRepository : BaseRepository<HabitacionAmaDeLLaves>, IHabitacionAmaDeLlavesRepository
    {
        public HabitacionAmaDeLlavesRepository(ApiDbContext context) : base(context)
        {
            
        }
        
    
    }
}
