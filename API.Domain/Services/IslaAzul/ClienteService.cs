using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.IslaAzul;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Validators.Seguridad;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Services.IslaAzul
{
    public class ClienteService : BasicService<Cliente, ClienteValidator>, IClienteService
    {

        public ClienteService(IUnitOfWork<Cliente> repositorios, IHttpContextAccessor httpContext) : base(repositorios,
            httpContext)
        {
            
        }

     
        public override async Task ValidarAntesEliminar(Guid id)
        {
            bool TieneReservas = await _repositorios.Reservas.AnyAsync(e => e.ClienteId == id);
            
            if (TieneReservas)
                throw new CustomException { Status = StatusCodes.Status400BadRequest, Message = "No se puede eliminar el cliente porque tiene reservas asociadas." };
            
            if (!await _repositorios.BasicRepository.AnyAsync(e => e.Id == id))
                throw new CustomException { Status = StatusCodes.Status404NotFound, Message = "Elemento no encontrado." };
        }


    }
}