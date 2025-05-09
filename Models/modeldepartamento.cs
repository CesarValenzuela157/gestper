using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;


public class Departamento
{
    [Key]
    public int IdDepartamento { get; set; }
    public string Nombre { get; set; }
}