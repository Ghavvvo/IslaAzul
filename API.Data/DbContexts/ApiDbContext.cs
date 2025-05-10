using API.Data.ConfiguracionEntidades.Seguridad;
using API.Data.Entidades.Seguridad;
using API.Data.ConfiguracionEntidades.IslaAzul;
using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DbContexts
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermiso { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<AmaDeLlaves> AmasDeLlaves { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; }
        
      
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RolPermisoConfiguracionBD.SetEntityBuilder(modelBuilder);
            RolConfiguracionBD.SetEntityBuilder(modelBuilder);
            PermisoConfiguracionBD.SetEntityBuilder(modelBuilder);
            UsuarioConfiguracionBD.SetEntityBuilder(modelBuilder);
            HabitacionConfiguracionBD.SetEntityBuilder(modelBuilder);
            AmaDeLlavesConfiguracionBD.SetEntityBuilder(modelBuilder);
            ClienteConfiguracionBD.SetEntityBuilder(modelBuilder);
            ReservaConfiguracionBD.SetEntityBuilder(modelBuilder);
            HabitacionAmaDeLlavesConfiguracionBD.SetEntityBuilder(modelBuilder);
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
