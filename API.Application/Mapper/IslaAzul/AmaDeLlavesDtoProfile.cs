using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class AmaDeLlavesDtoProfile : BaseProfile<AmaDeLlaves, AmaDeLlavesDto, CrearAmaDeLlavesInputDto, ActualizarAmaDeLlavesInputDto, ListadoPaginadoAmaDeLlavesDto>
    {
        public AmaDeLlavesDtoProfile()
        {
            MapDetallesAmaDeLlavesDto();
        }

        public void MapDetallesAmaDeLlavesDto()
        {
            CreateMap<AmaDeLlaves, DetallesAmaDeLlavesDto>().ReverseMap();
        }
       
    }
}


