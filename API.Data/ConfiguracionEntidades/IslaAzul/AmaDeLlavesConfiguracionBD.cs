using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.IslaAzul
{
    public class AmaDeLlavesConfiguracionBD
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmaDeLlaves>().ToTable("AmaDeLlavess");
            EntidadBaseConfiguracionBD<AmaDeLlaves>.SetEntityBuilder(modelBuilder);
            
            modelBuilder.Entity<AmaDeLlaves>().Property(e => e.Ci).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<AmaDeLlaves>().Property(e => e.Telefono).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Cliente>().HasIndex(e => new {e.Telefono}).IsUnique();
            modelBuilder.Entity<AmaDeLlaves>().HasIndex(e => new {e.Ci}).IsUnique();
            modelBuilder.Entity<AmaDeLlaves>().Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<AmaDeLlaves>().Property(e => e.Apellidos).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<AmaDeLlaves>().HasIndex(e => new { e.Nombre, e.Apellidos }).IsUnique();
            
        }
    }
}
