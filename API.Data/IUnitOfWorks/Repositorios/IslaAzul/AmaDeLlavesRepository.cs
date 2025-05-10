using API.Data.DbContexts;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Repositorios.Seguridad
{
    public class AmaDeLlavesRepository : BaseRepository<AmaDeLlaves>, IAmaDeLlavesRepository
    {
        public AmaDeLlavesRepository(ApiDbContext context) : base(context)
        {
            
        }
    }
}
