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
    public class AmaDeLlavesService : BasicService<AmaDeLlaves, AmaDeLlavesValidator>, IAmaDeLlavesService
    {

        public AmaDeLlavesService(IUnitOfWork<AmaDeLlaves> repositorios, IHttpContextAccessor httpContext) : base(repositorios,
            httpContext)
        {
        }
        
        


    }
}