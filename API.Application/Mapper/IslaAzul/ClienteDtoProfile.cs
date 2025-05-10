using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class ClienteDtoProfile : BaseProfile<Cliente, ClienteDto, CrearClienteInputDto, ActualizarClienteInputDto, ListadoPaginadoClienteDto>
    {
        public ClienteDtoProfile()
        {
            MapDetallesClienteDto();
        }

        public void MapDetallesClienteDto()
        {
            CreateMap<Cliente, DetallesClienteDto>().ReverseMap();
        }
       
    }
}


