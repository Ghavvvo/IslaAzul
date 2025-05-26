using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class AmaDeLlavesDtoProfile : BaseProfile<AmaDeLlaves, AmaDeLlavesDto, CrearAmaDeLlavesInputDto,
        ActualizarAmaDeLlavesInputDto, ListadoPaginadoAmaDeLlavesDto>
    {
        public AmaDeLlavesDtoProfile()
        {
            MapDetallesAmaDeLlavesDto();
            MapListadoDeHabitacionesDto();
        }

        public void MapDetallesAmaDeLlavesDto()
        {
            CreateMap<AmaDeLlaves, DetallesAmaDeLlavesDto>().ReverseMap();
        }
        
        public  void MapListadoDeHabitacionesDto()
        {
            CreateMap<AmaDeLlaves, ListadoDeHabitacionesDto>().ForMember(dest => dest.Habitaciones , opt =>
                opt.MapFrom(
                    src => src.HabitacionesAmasDeLLaves.Select(r => new DetallesHabitacionDto()
                            {
                                Id = r.HabitacionId,
                                Numero = r.Habitacion.Numero,
                            }
                        )
                        .Distinct()
                        .ToList()
                )).ReverseMap();
                        
        }

       
    }
}


