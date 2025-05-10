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
    public class HabitacionAmaDeLlavesService : BasicService<HabitacionAmaDeLLaves, HabitacionAmaDeLlavesValidator>, IHabitacionAmaDeLlavesService
    {

        public HabitacionAmaDeLlavesService(IUnitOfWork<HabitacionAmaDeLLaves> repositorios, IHttpContextAccessor httpContext) : base(repositorios,
            httpContext)
        {
        }
        
        public override async Task ValidarAntesCrear(HabitacionAmaDeLLaves entity)
        {   
            bool ExisteAsignacion = await _repositorios.HabitacionAmaDeLlaves.AnyAsync(e => e.HabitacionId == entity.HabitacionId && e.AmaDeLlavesId == entity.AmaDeLlavesId);

            if (ExisteAsignacion)
            {
                throw new CustomException
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Ya existe una asignación de ama de llaves a la habitación."
                };
            }
            
            //llamando al validador correspondiente 
            Type? objectType = Type.GetType(typeof(HabitacionAmaDeLlavesValidator).AssemblyQualifiedName ?? string.Empty);
            await (Activator.CreateInstance(objectType, _repositorios) as AbstractValidator<HabitacionAmaDeLLaves>).ValidateAndThrowAsync(entity);
        }


    }
}