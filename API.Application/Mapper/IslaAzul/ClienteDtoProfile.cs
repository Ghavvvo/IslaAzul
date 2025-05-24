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
            CreateMap<Cliente, ListadoPaginadoClienteHabitacionDto>().ForMember(dest => dest.Habitaciones, opt =>
                opt.MapFrom(
                    src => src.Reservas
                        .Select(r => new DetallesHabitacionDto()
                            {
                                Id = r.Habitacion.Id,
                                Numero = r.Habitacion.Numero,
                            }
                        )
                        .Distinct()
                        .ToList()
                )).ReverseMap();


        }
       
    }
}


