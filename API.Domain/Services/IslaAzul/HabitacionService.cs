using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Validators.Seguridad;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Services.IslaAzul
{
    public class HabitacionService : BasicService<Habitacion, HabitacionValidator>, IHabitacionService
    {
        public HabitacionService(IUnitOfWork<Habitacion> repositorios, IHttpContextAccessor httpContext) : base(
            repositorios, httpContext)
        {
        }
        
        public async Task<EntityEntry<Habitacion>> ActualizarHabitacionFuerdaDeServicio(Guid habitacionId)
        {
            
            var habitacionExistente = await ObtenerPorId(habitacionId);

            if (await _repositorios.Reservas.AnyAsync(r => r.HabitacionId == habitacionId))
            {
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "No se puede poner fuera de servicio porque la habitacion tiene reservas asociadas" };
            }
            if (habitacionExistente.EstaFueraDeServicio)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La habitacion seleccionada ya esta fuera de servicio" };
                
            
            habitacionExistente.EstaFueraDeServicio = true ;
            
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(habitacionExistente, esEntidadNueva: false));
           
        }

        public async Task<List<Habitacion>> ObtenerHabitacionesDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            IQueryable<Habitacion> query = _repositorios.BasicRepository.GetQuery();
          
            query = query.Where(r => !r.EstaFueraDeServicio  && !r.Reservas
                .Any(reserva => reserva.FechaEntrada.Date <= fechaFin.Date && reserva.FechaSalida.Date >= fechaInicio.Date && !reserva.EstaCancelada && (DateTime.Now.Date <= reserva.FechaEntrada.Date || reserva.EstaElClienteEnHostal)));
            
            var list = await query.Select(h => new Habitacion()
            {
                Id = h.Id,
                Numero = h.Numero
                
            }).ToListAsync();

            return list;

        }
    }
}
