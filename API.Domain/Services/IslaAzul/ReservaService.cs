using System.Linq.Expressions;
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
            
            
            if (reservaExistente.EstaElClienteEnHostal)
            {
                throw new CustomException {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede actualizar la reserva porque el cliente está en el hostal."
                };
            }

            
            if (DateTime.Now > reservaExistente.FechaEntrada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message =
                        "No se puede actualizar la reserva porque la fecha actual es mayor que la fecha de entrada."
                };
            }

            await ValidarDisponibilidad(reserva);

         
          
            
            //llamando al validador correspondiente
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>)
                .ValidateAndThrowAsync(reserva);


        }

        

        public  async Task ValidarDisponibilidad(Reserva reserva)
        {
            bool estaOcupada = await _repositorios.Reservas.AnyAsync(e =>
                                   e.HabitacionId == reserva.HabitacionId && 
                                   reserva.FechaEntrada < e.FechaSalida && reserva.FechaSalida > e.FechaEntrada
                               && !reserva.EstaCancelada && (DateTime.Now.Date <= reserva.FechaEntrada.Date || reserva.EstaElClienteEnHostal));
                
            if (estaOcupada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "La habitación seleccionada ya está ocupada en el rango de fechas especificado."
                };
            }

        }


        public override async Task ValidarAntesCrear(Reserva reserva)
        {         
           

            if (await _repositorios.Habitaciones.AnyAsync(e => e.Id == reserva.HabitacionId && e.EstaFueraDeServicio))
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "La habitacion selecionada esta fuera de servicio ."
                };
            }
            
            await ValidarDisponibilidad(reserva);

            //llamando al validador correspondiente
            Type? objectType = Type.GetType(typeof(ReservaValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<Reserva>)
                .ValidateAndThrowAsync(reserva);
        }

        public async Task<EntityEntry<Reserva>> RegistrarLlegadaReserva(Guid reservaId)
        {
            
            var reservaExistente = await ObtenerPorId(reservaId);

            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            if (reservaExistente.EstaElClienteEnHostal)
            {
                throw new CustomException {Status = StatusCodes.Status400BadRequest , Message = "El cliente ya se encuentra en el hostal"};
            }

            if (reservaExistente.EstaCancelada)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede registrar la llegada del cliente porque la reserva ya esta cancelada"
                };
            }

            if (DateTime.Now.Date != reservaExistente.FechaEntrada.Date)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede registrar la llegada de un cliente un dia diferente al de su entrada"
                };
            }
            
            reservaExistente.EstaElClienteEnHostal = true;
            
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(reservaExistente, esEntidadNueva: false));
           
        }
        
        public async Task<EntityEntry<Reserva>> CancelarReserva(Reserva entity)
        {   
            var reservaExistente = await ObtenerPorId(entity.Id);
           
            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };
            
            
            if (reservaExistente.EstaElClienteEnHostal)
            {
                throw new CustomException {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "No se puede actualizar la reserva porque el cliente está en el hostal."
                };
            }
            
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
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(reservaExistente, esEntidadNueva: false));
        }
        
        public  async Task<EntityEntry<Reserva>> CambiarDeHabitacion(Reserva entity)
        {   
            var reservaExistente = await ObtenerPorId(entity.Id);
           
            if (reservaExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La reserva seleccionada no existe" };

            reservaExistente.HabitacionId = entity.HabitacionId;
            
            await ValidarAntesActualizar(reservaExistente);
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(reservaExistente, esEntidadNueva: false));
        }
        
        public override async Task<EntityEntry<Reserva>> Crear(Reserva reserva)
        {   
            await ValidarAntesCrear(reserva);
            
            var cantidadDeDias =(decimal) reserva.FechaSalida.Day - reserva.FechaEntrada.Day + 1;
            decimal importe = cantidadDeDias * 10;

            var cliente = await _repositorios.Clientes.FirstAsync(c => c.Id == reserva.ClienteId);
            if (cliente == null)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest, Message = "El cliente no esta registrado en el sistema"
                };
            }
            if (cliente.EsVip)
            {
                importe -= importe * (decimal)0.10;
            }

            reserva.ImporteDeRenta = importe;
            
           
            return await _repositorios.BasicRepository.AddAsync(EstablecerDatosAuditoria(reserva));
        }
    }
}




