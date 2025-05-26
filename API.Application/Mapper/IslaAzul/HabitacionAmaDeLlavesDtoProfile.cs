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
       
        public override void MapListEntityDto()
        {
            CreateMap<HabitacionAmaDeLLaves, ListadoPaginadoHabitacionAmaDeLlavesDto>()
                .ForMember(dto => dto.Habitacion, opt => opt.MapFrom(e => e.Habitacion.Numero))
                .ForMember(dto => dto.AmaDeLlaves, opt => opt.MapFrom(e => e.AmaDeLlaves.Nombre + " " + e.AmaDeLlaves.Apellidos))

                .ReverseMap();
        }

    
        
       
    }
}


