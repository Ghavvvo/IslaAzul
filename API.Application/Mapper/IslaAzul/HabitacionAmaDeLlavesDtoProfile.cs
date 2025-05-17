using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class HabitacionAmaDeLlavesDtoProfile : BaseProfile<HabitacionAmaDeLLaves, HabitacionAmaDeLlavesDto, CrearHabitacionAmaDeLlavesInputDto, ActualizarHabitacionAmaDeLlavesInputDto, ListadoPaginadoHabitacionAmaDeLlavesDto>
    {
        public HabitacionAmaDeLlavesDtoProfile()
        {
            MapDetallesHabitacionAmaDeLlavesDto();
        }

        public void MapDetallesHabitacionAmaDeLlavesDto()
        {
            CreateMap<HabitacionAmaDeLLaves, DetallesHabitacionAmaDeLlavesDto>().ReverseMap();
        }
       
    }
}


