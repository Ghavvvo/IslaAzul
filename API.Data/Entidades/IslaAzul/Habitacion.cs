namespace API.Data.Entidades.IslaAzul;

public class Habitacion : EntidadBase
{
    public String Numero { get; set; } = string.Empty;
   
    public bool EstaFueraDeServicio { get; set; }
    
    public List<Reserva> Reservas { get; set; } = new();
    
    public List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();

}