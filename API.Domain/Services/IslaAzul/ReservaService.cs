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
            var reservaExistente = await ObtenerPorId(reserva.Id);
            
            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            if (DateTime.Now > reservaExistente.FechaEntrada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message =
                        "No se puede actualizar la reserva porque la fecha actual es mayor que la fecha de entrada."
                };
            }

            if (reservaExistente.EstaElClienteEnHostal)
             {
                 throw new CustomException {
                     Status = StatusCodes.Status400BadRequest,
                     Message = "No se puede actualizar la reserva porque el cliente está en el hostal."
                 };
             }

          
            
            //llamando al validador correspondiente
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>)
                .ValidateAndThrowAsync(reserva);


        }


        public override async Task ValidarAntesCrear(Reserva reserva)
        {
            bool EstaDuplicada = await _repositorios.Reservas.AnyAsync(e =>
                e.Id != reserva.Id && e.ClienteId == reserva.ClienteId && e.HabitacionId == reserva.HabitacionId &&
                e.FechaEntrada == reserva.FechaEntrada && e.FechaSalida == reserva.FechaSalida);

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

            bool cliente = await _repositorios.Clientes.AnyAsync(e =>
                e.Id == reserva.ClienteId);

            if (!cliente)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "El cliente selecionado no esta registrado en el sistema ."
                };
            }

            var habitacion = await _repositorios.Habitaciones.FirstAsync(h => h.Id == reserva.HabitacionId);

            if (habitacion == null)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "La habitacion selecionada no esta registrada en el sistema ."
                };
            }

            if (habitacion.EstaFueraDeServicio)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "La habitacion selecionada esta fuera de servicio ."
                };
            }


            //llamando al validador correspondiente
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>)
                .ValidateAndThrowAsync(reserva);
        }

        public async Task<EntityEntry<Reserva>> ActualizarRegistrarLlegadaReserva(Reserva reserva)
        {
            
            var reservaExistente = await ObtenerPorId(reserva.Id);

            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            if (reservaExistente.EstaElClienteEnHostal)
            {
                throw new CustomException {Status = StatusCodes.Status400BadRequest , Message = "El cliente ya se encuentra en el hostal"};
            }
            
            if(!reserva.EstaElClienteEnHostal && reservaExistente.EstaElClienteEnHostal)
            {
                throw new CustomException {Status = StatusCodes.Status400BadRequest , Message = "Operacion no permitida.El cliente ya esta registro su llegada al hostal"};
            }
            
            reservaExistente.EstaElClienteEnHostal = reserva.EstaElClienteEnHostal;
            
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(reservaExistente, esEntidadNueva: false));
           
        }
        
        public async Task<EntityEntry<Reserva>> CancelarReserva(Reserva entity)
        {   
            var reservaExistente = await ObtenerPorId(entity.Id);
           
            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            await ValidarAntesActualizar(reservaExistente);
            
            reservaExistente.FechaCancelacion = DateTime.Now;
            reservaExistente.MotivoCancelacion = entity.MotivoCancelacion;
            reservaExistente.EstaCancelada = true;
         
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(reservaExistente, esEntidadNueva: false));
        }
        
        public override async Task<EntityEntry<Reserva>> Actualizar(Reserva entity)
        {   
            var reservaExistente = await ObtenerPorId(entity.Id);
           
            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            await ValidarAntesActualizar(reservaExistente);
            
            reservaExistente.FechaEntrada = entity.FechaEntrada;
            reservaExistente.FechaSalida = entity.FechaSalida;
            reservaExistente.ClienteId = entity.ClienteId;
            reservaExistente.HabitacionId = entity.HabitacionId;
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(entity, esEntidadNueva: false));
        }
    }
}




