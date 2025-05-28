using System.Text.Json.Serialization;
using API.Application.Dtos.Comunes;
using API.Data.Entidades;
using API.Data.Entidades.IslaAzul;

namespace API.Application.Dtos.Seguridad.Usuario
{
    public class HabitacionesOcupadasDto : EntidadBaseDto 
    {
        public String Numero { get; set; } = string.Empty;
        
     
        
    }
}
