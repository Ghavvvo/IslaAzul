using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Validators.Seguridad;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Services.Seguridad
{
    public class ReservaService : BasicService<Reserva, ReservaValidator>, IReservaService
    {

        public ReservaService(IUnitOfWork<Reserva> repositorios, IHttpContextAccessor httpContext) : base(repositorios,
            httpContext)
        {
        }
        
        
        public override async Task ValidarAntesActualizar(Reserva reserva)
        {   
            if ( DateTime.Now > reserva.FechaEntrada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede actualizar la reserva porque la fecha de entrada es mayor que la fecha actual."
                };
            }

            if (!reserva.EstaElClienteEnHostal)
            {
                throw new CustomException {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede actualizar la reserva porque el cliente está en el hostal."
                };
            }
            
            //llamando al validador correspondiente 
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>).ValidateAndThrowAsync(reserva);
        

        }
        
        public override async Task ValidarAntesCrear(Reserva reserva)
        {   
            bool EstaDuplicada = await _repositorios.Reservas.AnyAsync(e => e.Id != reserva.Id && e.ClienteId == reserva.ClienteId && e.HabitacionId == reserva.HabitacionId && e.FechaEntrada == reserva.FechaEntrada && e.FechaSalida == reserva.FechaSalida);
            
            if (EstaDuplicada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Ya existe una reserva para esta habitación y cliente en el rango de fechas especificado."
                };
            }

            bool EstaOcupada = await _repositorios.Reservas.AnyAsync(e =>
                e.HabitacionId == reserva.HabitacionId && e.FechaEntrada < reserva.FechaEntrada &&
                reserva.FechaEntrada < e.FechaSalida ||
                e.FechaEntrada < reserva.FechaSalida && reserva.FechaSalida < e.FechaEntrada);
            
            if (EstaOcupada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "La habitación seleccionada ya está ocupada en el rango de fechas especificado."
                };
            }
            //llamando al validador correspondiente 
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>).ValidateAndThrowAsync(reserva);
        }


    }
}