using API.Application.Dtos.Comunes;
using API.Application.Validadotors.Seguridad;
using API.Data.Entidades.Seguridad;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Validators.Seguridad;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Domain.Interfaces.IslaAzul;

namespace API.Application.Controllers.IslaAzul
{
    public class AmaDeLlavesController : BasicController<AmaDeLlaves, AmaDeLlavesValidator, DetallesAmaDeLlavesDto, CrearAmaDeLlavesInputDto
        , ActualizarAmaDeLlavesInputDto, ListadoPaginadoAmaDeLlavesDto, FiltrarConfigurarListadoPaginadoAmaDeLlavesIntputDto>
    {
        public AmaDeLlavesController(IMapper mapper, IAmaDeLlavesService servicioAmaDeLlaves, IHttpContextAccessor httpContext) :
            base(mapper, servicioAmaDeLlaves, httpContext)
        {
            
        }

      

        [HttpGet("[action]/{id}")]
        public  async Task<IActionResult> ObtenerHabitacionesDeAmaDeLlaves(Guid id)
        {
            _servicioBase.ValidarPermisos("listar, gestionar");

            AmaDeLlaves?  entity = await _servicioBase.ObtenerPorId(id , propiedadesIncluidas: queryable => queryable.Include(a => a.HabitacionesAmasDeLLaves).ThenInclude(a => a.Habitacion)   );
            
            ListadoDeHabitacionesDto entityDto = _mapper.Map<ListadoDeHabitacionesDto>(entity);

            return Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = entityDto });
        }
       
        
        
    }
}