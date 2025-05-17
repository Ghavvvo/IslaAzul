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
        
        public async Task<EntityEntry<Habitacion>> ActualizarHabitacionFuerdaDeServicio(Habitacion habitacion)
        {
            
            var habitacionExistente = await ObtenerPorId(habitacion.Id);

            if (habitacionExistente == null)
                throw new CustomException
                    { Status = StatusCodes.Status400BadRequest, Message = "La habitacion seleccionada no existe" };
            
            
            habitacionExistente.EstaFueraDeServicio = habitacion.EstaFueraDeServicio;
            
            
            return _repositorios.BasicRepository.Update(EstablecerDatosAuditoria(habitacionExistente, esEntidadNueva: false));
           
        }

       

        
    }
}