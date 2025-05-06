using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;


public class Asignacion
{
    [Key] public int IdAsignacion { get; set; }
    
    public string Idticket { get; set; }
    public string Idusuario { get; set; }
    public DateTime Feshaasignacion { get; set; }
}
