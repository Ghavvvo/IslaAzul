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

        public override async Task<(IEnumerable<HabitacionAmaDeLLaves>, int)> ObtenerListadoPaginado(int cantidadIgnorar, int? cantidadMostrar, string? secuenciaOrdenamiento, Func<IQueryable<HabitacionAmaDeLLaves>, IIncludableQueryable<HabitacionAmaDeLLaves, object>>? propiedadesIncluidas = null, params Expression<Func<HabitacionAmaDeLLaves, bool>>[] filtros)
        {
            if (cantidadIgnorar < 0)
                throw new CustomException { Status = StatusCodes.Status404NotFound, Message = "La cantidad de elementos a ignorar debe ser mayor o igual a 0." };
            if (cantidadMostrar.HasValue && cantidadMostrar.Value <= 0)
                throw new CustomException { Status = StatusCodes.Status404NotFound, Message = "La cantidad de elementos a mostrar debe ser mayor a 0." };

            IQueryable<HabitacionAmaDeLLaves> query = _repositorios.BasicRepository.GetQuery();

            //Filtrando
            query = filtros.Aggregate(query, (current, filters) => current.Where(filters));
            //Ordenando
            query = OrdenarLista(query, secuenciaOrdenamiento);
            //Counting
            int total = await query.CountAsync();
            //Paginando
            query = query.Skip(cantidadIgnorar).Take(cantidadMostrar.GetValueOrDefault(total));

            var list = await query.Select(e => new HabitacionAmaDeLLaves
            {
                Id = e.Id,
                HabitacionId = e.HabitacionId,
                AmaDeLlavesId = e.AmaDeLlavesId,
                Habitacion = new Habitacion()
                {
                    Numero = e.Habitacion.Numero
                },
                AmaDeLlaves = new AmaDeLlaves()
                {
                    Nombre = e.AmaDeLlaves.Nombre,
                    Apellidos = e.AmaDeLlaves.Apellidos
                }
            }).ToListAsync();

            return (list, total);
        }

    }
}