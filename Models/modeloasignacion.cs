using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;


public class Asignacion
{
    [Key] public int IdAsignacion { get; set; }
    
    public string IdTicket { get; set; }
    public string IdUsuario { get; set; }
    public DateTime FeshaAsignacion { get; set; }
}
