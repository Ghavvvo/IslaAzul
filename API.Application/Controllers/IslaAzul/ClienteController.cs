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
using Microsoft.EntityFrameworkCore.Query;

namespace API.Application.Controllers.IslaAzul
{
    public class ClienteController : BasicController<Cliente, ClienteValidator, DetallesClienteDto, CrearClienteInputDto
        , ActualizarClienteInputDto, ListadoPaginadoClienteDto, FiltrarConfigurarListadoPaginadoClienteIntputDto>
    {
        public ClienteController(IMapper mapper, IClienteService servicioCliente, IHttpContextAccessor httpContext) :
            base(mapper, servicioCliente, httpContext)
        {
            
        }
        
        [HttpGet("[action]")]
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
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerClientesConHabitacionesDeReserva([FromQuery] FiltrarConfigurarListadoPaginadoClienteHabitacionDto? filtrarDto)
        {    
            if (filtrarDto?.Fecha == null)
            {
                return BadRequest(new ResponseDto 
                { 
                    Status = StatusCodes.Status400BadRequest, 
                    ErrorMessage = "El filtro de fecha es obligatorio." 
                });
            }
            
            _servicioBase.ValidarPermisos("listar, gestionar");

            (IEnumerable<Cliente> listado, int cantidad) = await AplicarFiltrosIncluirPropiedadesClienteHabitacion(filtrarDto);

            ListadoPaginadoDto<ListadoPaginadoClienteHabitacionDto> listadoPaginadoDto = new()
            {
                Elementos = _mapper.Map<List<ListadoPaginadoClienteHabitacionDto>>(listado),
                Cantidad = cantidad
            };

            return Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = listadoPaginadoDto });
        }
        
      
        
        protected Task<(IEnumerable<Cliente>, int)> AplicarFiltrosIncluirPropiedadesClienteHabitacion(FiltrarConfigurarListadoPaginadoClienteHabitacionDto? inputDto)
        {
            
            List<Expression<Func<Cliente, bool>>> filtros = new();
            
            Func<IQueryable<Cliente>, IIncludableQueryable<Cliente, object>> propiedadesIncluidas;

             
                filtros.Add(cliente => cliente.Reservas.Any(r => 
                    r.FechaEntrada.Date <= inputDto.Fecha.Value.Date && 
                    r.FechaSalida.Date >= inputDto.Fecha.Value.Date &&
                    !r.EstaCancelada)); 
                
                propiedadesIncluidas =query => query.Include(c => c.Reservas.Where(r =>
                            r.FechaEntrada.Date <= inputDto.Fecha.Value.Date &&
                            r.FechaSalida.Date >= inputDto.Fecha.Value.Date &&
                            !r.EstaCancelada))
                        .ThenInclude(r => r.Habitacion);
                
                

         
            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar,
                inputDto.SecuenciaOrdenamiento, propiedadesIncluidas, filtros.ToArray());
        }
        
       

        
    }
}