using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Domain.Validators.Seguridad;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Domain.Interfaces.IslaAzul
{
    public interface IClienteService : IBaseService<Cliente, ClienteValidator>
    {
        
    }
}