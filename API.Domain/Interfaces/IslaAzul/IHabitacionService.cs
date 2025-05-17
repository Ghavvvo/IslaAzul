using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Domain.Validators.Seguridad;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Interfaces.Seguridad
{
    public interface IHabitacionService : IBaseService<Habitacion, HabitacionValidator>
    {
        Task<EntityEntry<Habitacion>> ActualizarHabitacionFuerdaDeServicio(Habitacion habitacion);
    }
}