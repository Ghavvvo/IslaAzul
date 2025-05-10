using System;
using API.Data.Entidades.IslaAzul;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.IslaAzul
{
    public class HabitacionConfiguracionBD
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habitacion>().ToTable("Habitaciones");
            EntidadBaseConfiguracionBD<Habitacion>.SetEntityBuilder(modelBuilder);
           
            modelBuilder.Entity<Habitacion>().Property(e => e.Numero).HasMaxLength(3).IsRequired();
            modelBuilder.Entity<Habitacion>().HasIndex(e => new { e.Numero }).IsUnique();

            #region Seed

            modelBuilder.Entity<Habitacion>().HasData(
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264341"), Numero = "011" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264342"), Numero = "012" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264343"), Numero = "013" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264344"), Numero = "014" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264345"), Numero = "015" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264346"), Numero = "021" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264347"), Numero = "022" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264348"), Numero = "023" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264349"), Numero = "024" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264350"), Numero = "025" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264351"), Numero = "031" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264352"), Numero = "032" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264353"), Numero = "033" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264354"), Numero = "034" },
                new Habitacion { Id = new Guid("8E0A53D7-E986-4649-A9E2-6E92E0264355"), Numero = "035" }
            );

            #endregion


        }
        
    }
}
