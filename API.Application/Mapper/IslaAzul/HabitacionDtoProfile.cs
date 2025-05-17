using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class HabitacionDtoProfile : BaseProfile<Habitacion, HabitacionDto, CrearHabitacionInputDto, ActualizarHabitacionInputDto, ListadoPaginadoHabitacionDto>
    {
        public HabitacionDtoProfile()
        {
            MapDetallesHabitacionDto();
            MapEstaFuertaDeServicioDto();
        }

        public void MapDetallesHabitacionDto()
        {
            CreateMap<Habitacion, DetallesHabitacionDto>().ReverseMap();
        }

        public void MapEstaFuertaDeServicioDto()
        {
            CreateMap<Habitacion, EstFueraDeServicioHabitacionInputDto>().ReverseMap() ;
        }
       
    }
}


