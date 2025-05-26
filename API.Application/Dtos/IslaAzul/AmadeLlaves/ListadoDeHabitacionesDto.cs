namespace API.Application.Dtos.Seguridad.Usuario
{
    public class ListadoDeHabitacionesDto 
    {
        public List<DetallesHabitacionDto> Habitaciones { get; set; } = new();
    }
}
