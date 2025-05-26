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
    public class HabitacionAmaDeLlavesController : BasicController<HabitacionAmaDeLLaves, HabitacionAmaDeLlavesValidator, DetallesHabitacionAmaDeLlavesDto, CrearHabitacionAmaDeLlavesInputDto
        , ActualizarHabitacionAmaDeLlavesInputDto, ListadoPaginadoHabitacionAmaDeLlavesDto, FiltrarConfigurarListadoPaginadoHabitacionAmaDeLlavesIntputDto>
    {
        public HabitacionAmaDeLlavesController(IMapper mapper, IHabitacionAmaDeLlavesService servicioHabitacionAmaDeLlaves, IHttpContextAccessor httpContext) :
            base(mapper, servicioHabitacionAmaDeLlaves, httpContext)
        {
            
        }
        
     
        

    }
}