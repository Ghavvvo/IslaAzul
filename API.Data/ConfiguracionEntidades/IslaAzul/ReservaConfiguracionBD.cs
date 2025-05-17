using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.IslaAzul
{
    public class ReservaConfiguracionBD
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reserva>().ToTable("Reservas");
            EntidadBaseConfiguracionBD<Reserva>.SetEntityBuilder(modelBuilder);

           
            
            modelBuilder.Entity<Reserva>().Property(e => e.FechaReservacion ).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.FechaEntrada).IsRequired();

            modelBuilder.Entity<Reserva>().Property(e => e.FechaSalida).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.ImporteDeRenta).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.ClienteId).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.HabitacionId).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.EstaElClienteEnHostal).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.EstaCancelada).IsRequired();
            
            modelBuilder.Entity<Reserva>().Property(e => e.MotivoCancelacion).HasMaxLength(500);
            
            
            
            modelBuilder.Entity<Reserva>().HasOne(e => e.Cliente).WithMany(e => e.Reservas).HasForeignKey(e => e.ClienteId).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Reserva>().HasOne(e => e.Habitacion).WithMany(e => e.Reservas).HasForeignKey(e => e.HabitacionId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
