using API.Data.DbContexts;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Repositorios.Seguridad
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
