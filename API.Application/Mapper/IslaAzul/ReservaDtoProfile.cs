using API.Application.Dtos.IslaAzul.Reserva;
using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class ReservaDtoProfile : BaseProfile<Reserva, ReservaDto, CrearReservaInputDto, ActualizarReservaInputDto, ListadoPaginadoReservaDto>
    {
        public ReservaDtoProfile()
        {
            MapDetallesReservaDto();
            MapRegistroReservaDto();
            MapCancelarReservaDto();
        }

        public void MapDetallesReservaDto()
        {
            CreateMap<Reserva, DetallesReservaDto>().ReverseMap();
        }

        public void MapRegistroReservaDto()
        {
            CreateMap<Reserva,RegistrarLlegadaReservaInputDto>().ReverseMap();
        }
        
        public void MapCancelarReservaDto()
        {
            CreateMap<Reserva,CancelarReservaInputDto>().ReverseMap();
        }


    }
}


