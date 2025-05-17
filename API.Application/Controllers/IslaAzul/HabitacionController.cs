using API.Application.Dtos.Comunes;
using API.Application.Validadotors.Seguridad;
using API.Data.Entidades.Seguridad;
using API.Domain.Services.Seguridad;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Validators.Seguridad;
using API.Domain.Services.IslaAzul;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Domain.Exceptions;
using API.Domain.Interfaces.IslaAzul;
using API.Domain.Services.IslaAzul;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Application.Controllers.IslaAzul
{
    public class HabitacionController : BasicController<Habitacion, HabitacionValidator, DetallesHabitacionDto, CrearHabitacionInputDto
        , ActualizarHabitacionInputDto, ListadoPaginadoHabitacionDto, FiltrarConfigurarListadoPaginadoHabitacionIntputDto>
    {
        public HabitacionController(IMapper mapper, IHabitacionService servicioHabitacion, IHttpContextAccessor httpContext) :
            base(mapper, servicioHabitacion, httpContext)
        {
            
        }
        
        [HttpPut("[action]/{id}")]
        public  async Task<IActionResult> ActualizarHabitacionFueraDeServicio(Guid id, EstFueraDeServicioHabitacionInputDto actualizarDto)
        {
            if (id != actualizarDto.Id)
                return BadRequest(new ResponseDto { Status = StatusCodes.Status400BadRequest, ErrorMessage = "Error al actualizar" });
            _servicioBase.ValidarPermisos("gestionar");

            Habitacion habitacion = _mapper.Map<Habitacion>(actualizarDto);
            
            EntityEntry<Habitacion> result = await ((IHabitacionService)_servicioBase).ActualizarHabitacionFuerdaDeServicio(habitacion);;
            
            await _servicioBase.GuardarTraza(usuario, $"Actualizado elemento con id = {result.Entity.Id} en la tabla {typeof(Habitacion).Name}s", typeof(Habitacion).Name);
            await _servicioBase.SalvarCambios();

            EstFueraDeServicioHabitacionInputDto entityDto = _mapper.Map<EstFueraDeServicioHabitacionInputDto>(result.Entity);
            
            return Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = entityDto });
        }
        
      
        
        

        
    }
}
