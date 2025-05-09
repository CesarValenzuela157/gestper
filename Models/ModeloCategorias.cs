using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class Categoria
{

    [Key] public int IdCategoria { get; set; }
    public string Nombre { get; init; }
}