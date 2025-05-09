using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class Prioridad
{
    [Key]
    public int IdPrioridad { get; set; }
    public string NombrePrioridad { get; set; }
}