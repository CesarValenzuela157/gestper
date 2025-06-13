using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestper.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
    public string Correo { get; set; }

    public string Contrasena { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Debe ingresar solo números (9 dígitos).")]
    public string Telefono { get; set; }

    public int IdRol { get; set; }

    public bool Activo { get; set; }

    public int? IdDepartamento { get; set; }

    [ForeignKey("IdDepartamento")]
    public Departamento? Departamento { get; set; }

}