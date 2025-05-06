using Microsoft.AspNetCore.Mvc;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class HomeControllerAdmin : Controller
    {
        public IActionResult Index()
        {
            var tickets = new List<TicketAdmin>
            {
                new TicketAdmin
                {
                    Id = 1,
                    NombreConsulta = "Ejemplo de consulta",
                    Descripcion = "Esto es una descripción de prueba",
                    Estado = "Nuevo",
                    Prioridad = "Alta",
                    Departamento = "Soporte",
                    CreadoPor = "Juan",
                    FechaCreacion = DateTime.Now
                }
            };

            return View("IndexAdmin", tickets);
        }


        public IActionResult Detalle()
        {
            return View("DetalleTicket");
        }
    }
}