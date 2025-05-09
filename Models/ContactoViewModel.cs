using System.ComponentModel.DataAnnotations;

namespace Gestper.Models;

public class ContactoViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "El mensaje es obligatorio.")]
    public string Mensaje { get; set; }
}