using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class modeloestado
{
    
}

public class Estado
{
    [Key]
    public int Idestado { get; set; }
    public string Nombreestado { get; set; }
}