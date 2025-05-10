namespace API.Data.Entidades.IslaAzul;

public class AmaDeLlaves  : EntidadBase
{   
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Ci { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;
    public List<HabitacionAmaDeLLaves> HabitacionesAmasDeLLaves { get; set; } = new();
}