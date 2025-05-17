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
        
        
    }
}