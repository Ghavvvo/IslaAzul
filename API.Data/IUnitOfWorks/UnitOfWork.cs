using API.Data.DbContexts;
using API.Data.Entidades;
using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;
using API.Data.IUnitOfWorks.Repositorios;
using API.Data.IUnitOfWorks.Repositorios.Seguridad;
using IAmaDeLlavesRepository = API.Data.IUnitOfWorks.Interfaces.Seguridad.IAmaDeLlavesRepository;
using IClienteRepository = API.Data.IUnitOfWorks.Interfaces.Seguridad.IClienteRepository;
using IHabitacionAmaDeLlavesRepository = API.Data.IUnitOfWorks.Interfaces.Seguridad.IHabitacionAmaDeLlavesRepository;
using IReservaRepository = API.Data.IUnitOfWorks.Interfaces.Seguridad.IReservaRepository;

namespace API.Data.IUnitOfWorks
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : EntidadBase
    {
        private readonly ApiDbContext _context;
        private readonly TrazasDbContext _trazasContext;

        
        public IAmaDeLlavesRepository AmaDeLlaves { get; }
        
        public IReservaRepository Reservas { get; }
        
        public IHabitacionAmaDeLlavesRepository HabitacionAmaDeLlaves { get; }
        
        public IClienteRepository Clientes { get; }
        public IHabitacionRepository Habitaciones { get; }
        public IPermisoRepository Permisos { get; }
        public IRolPermisoRepository RolesPermisos { get; }
        public IRolRepository Roles { get; }
        public IUsuarioRepository Usuarios { get; }
        public ITrazaRepository Trazas { get; }
        public IBaseRepository<TEntity> BasicRepository { get; }

        public UnitOfWork(ApiDbContext context, TrazasDbContext trazasContext)
        {
            _context = context;
            _trazasContext = trazasContext;
            
            Habitaciones = new HabitacionRepository(context);
            Reservas = new ReservaRepository(context);
            Clientes = new ClienteRepository(context);
            AmaDeLlaves = new AmaDeLlavesRepository(context);
            HabitacionAmaDeLlaves = new HabitacionAmaDeLlavesRepository(context);
            Permisos = new PermisoRepository(context);
            RolesPermisos = new RolPermisoRepository(context);
            Roles = new RolRepository(context);
            Usuarios = new UsuarioRepository(context);
            Trazas = new TrazaRepository(trazasContext);
            BasicRepository = new BaseRepository<TEntity>(context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => await _context.SaveChangesAsync(cancellationToken);
        public async Task<int> SaveTrazasChangesAsync(CancellationToken cancellationToken = default)
           => await _trazasContext.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();


    }
}
