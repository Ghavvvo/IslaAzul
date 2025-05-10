namespace API.Data.Entidades.IslaAzul;

public class HabitacionAmaDeLLaves : EntidadBase
{
    public Guid HabitacionId { get; set; }
    public Habitacion Habitacion { get; set; } = null!;
    public Guid AmaDeLlavesId { get; set; }
    public AmaDeLlaves AmaDeLlaves { get; set; } = null!;
    
}