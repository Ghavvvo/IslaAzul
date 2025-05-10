namespace API.Data.Entidades.IslaAzul;

public class Cliente :  EntidadBase
{    
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    
    public bool EsVip { get; set; } 
    
    public string Ci { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;
    
    public List<Reserva> Reservas { get; set; } = new();
}