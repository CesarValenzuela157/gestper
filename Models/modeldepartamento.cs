using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class modeldepartamento
{
    
}

public class Departamento
{
    [Key]
    public int Iddepartamento { get; set; }
    public string Nombredepartamento { get; set; }
}