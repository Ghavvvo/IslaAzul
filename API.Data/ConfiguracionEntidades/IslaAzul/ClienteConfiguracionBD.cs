using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.IslaAzul
{
    public class ClienteConfiguracionBD
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            EntidadBaseConfiguracionBD<Cliente>.SetEntityBuilder(modelBuilder);
            modelBuilder.Entity<Cliente>().Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Cliente>().Property(e => e.Ci).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Cliente>().HasIndex(e => new {e.Ci}).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(e => new {e.Telefono}).IsUnique();
            modelBuilder.Entity<Cliente>().Property(e => e.Telefono).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Cliente>().Property(e => e.Apellidos).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Cliente>().HasIndex(e => new { e.Nombre, e.Apellidos }).IsUnique();
            
        }
    }
}
