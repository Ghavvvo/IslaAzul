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
    public class ClienteController : BasicController<Cliente, ClienteValidator, DetallesClienteDto, CrearClienteInputDto
        , ActualizarClienteInputDto, ListadoPaginadoClienteDto, FiltrarConfigurarListadoPaginadoClienteIntputDto>
    {
        public ClienteController(IMapper mapper, IClienteService servicioCliente, IHttpContextAccessor httpContext) :
            base(mapper, servicioCliente, httpContext)
        {
            
        }
        

        protected override Task<(IEnumerable<Cliente>, int)> AplicarFiltrosIncluirPropiedades(
            FiltrarConfigurarListadoPaginadoClienteIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Cliente, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
            {
                filtros.Add(cliente => cliente.Nombre.Contains(inputDto.TextoBuscar) ||
                                       cliente.Apellidos.Contains(inputDto.TextoBuscar) ||
                                       cliente.Ci.Contains(inputDto.TextoBuscar) ||
                                       cliente.Telefono.Contains(inputDto.TextoBuscar));


            }

            if (!string.IsNullOrEmpty(inputDto.Nombre))
            {
                filtros.Add(cliente => cliente.Nombre.Contains(inputDto.Nombre));
            }
            if (!string.IsNullOrEmpty(inputDto.Apellidos))
            {
              filtros.Add(cliente => cliente.Apellidos.Contains(inputDto.Apellidos));
            }
            if (!string.IsNullOrEmpty(inputDto.Ci))
            {
                filtros.Add(cliente => cliente.Ci.Contains(inputDto.Ci));
            }
            if (!string.IsNullOrEmpty(inputDto.Telefono))
            {
                filtros.Add(cliente => cliente.Telefono.Contains(inputDto.Telefono));
            }
            

            //IIncludableQueryable<Usuario, object> propiedadesIncluidas(IQueryable<Usuario> query) => query.Include(e => e.ShipmentItems);

            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar,
                inputDto.SecuenciaOrdenamiento, null, filtros.ToArray());
        }
    }
}