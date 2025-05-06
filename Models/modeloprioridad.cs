using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class modeloprioridad
{
    
}

public class prioridad
{
    [Key]
    public int Idprioridad { get; set; }
    public string Nombreprioridad { get; set; }
}