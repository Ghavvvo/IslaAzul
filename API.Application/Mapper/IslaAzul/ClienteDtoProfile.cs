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
            MapListadoPaginadoClienteHabitacionDto();
        }

        public void MapDetallesClienteDto()
        {
            CreateMap<Cliente, DetallesClienteDto>().ReverseMap();
        }

        public void MapListadoPaginadoClienteHabitacionDto()
        {
            CreateMap<Cliente, ListadoPaginadoClienteHabitacionDto>()
                .ForMember(dest => dest.Cliente, opt =>
                    opt.MapFrom(src => src.Nombre + " " + src.Apellidos))
           
                .ForMember(dest => dest.FechaEntrada, opt =>
                    opt.MapFrom(src => src.Reservas
                        .Select(r => r.FechaEntrada)
                        .FirstOrDefault()))
                .ForMember(dest => dest.FechaSalida, opt =>
                    opt.MapFrom(src => src.Reservas
                        .Select(r => r.FechaSalida)
                        .FirstOrDefault()))
                
                .ForMember(dest => dest.Habitacion, opt =>
                opt.MapFrom(
                    src => src.Reservas
                        .Select(r => new HabitacionesOcupadasDto()
                        {
                            Id = r.Habitacion.Id,
                            Numero = r.Habitacion.Numero,
                        })
                        .FirstOrDefault() 
                )).ReverseMap();
            
            
                


        }
       
    }
}


