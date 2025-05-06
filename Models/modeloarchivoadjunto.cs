using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class modeloarchivoadjunto
{
    
}

public class Archivoadjunto
{
    [Key]
    public int Idarchivo { get; set; }
    public int IDticket { get; set; }
    public string Idusuario { get; set; }
    public string Feshaasignacion { get; set; }
}
