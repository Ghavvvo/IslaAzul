using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.IslaAzul
{
    public class HabitacionAmaDeLlavesConfiguracionBD
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HabitacionAmaDeLLaves>().ToTable("HabitacionesAmasDeLlaves");
            EntidadBaseConfiguracionBD<HabitacionAmaDeLLaves>.SetEntityBuilder(modelBuilder);

           
            modelBuilder.Entity<HabitacionAmaDeLLaves>().Property(e => e.HabitacionId).IsRequired();
            modelBuilder.Entity<HabitacionAmaDeLLaves>().Property(e => e.AmaDeLlavesId).IsRequired();

            modelBuilder.Entity<HabitacionAmaDeLLaves>().HasIndex(e=> new { e.HabitacionId, e.AmaDeLlavesId }).IsUnique();
            modelBuilder.Entity<HabitacionAmaDeLLaves>().HasOne(e => e.Habitacion).WithMany(e => e.HabitacionesAmasDeLLaves).HasForeignKey(e => e.HabitacionId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HabitacionAmaDeLLaves>().HasOne(e  => e.AmaDeLlaves).WithMany(e => e.HabitacionesAmasDeLLaves).HasForeignKey(e => e.AmaDeLlavesId).OnDelete(DeleteBehavior.Cascade);
            
          
        }
    }
}
