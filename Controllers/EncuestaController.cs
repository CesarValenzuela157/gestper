using Microsoft.AspNetCore.Mvc;
using gestper.Models; // Asegúrate de tener esta referencia para tu modelo

namespace gestper.Controllers
{
    public class EncuestaController : Controller
    {
        // Muestra el formulario
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Procesa el formulario
        [HttpPost]
        public IActionResult Create(EncuestaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí podrías guardar los datos si tuvieras una base de datos
                // Redirige a la vista "Gracias", pasando el modelo como parámetro
                return View("Gracias", model);
            }

            // Si hay errores de validación, vuelve a mostrar el formulario con errores
            return View(model);
        }

        // Vista de agradecimiento (se usará cuando se llama directamente o con un modelo)
        [HttpGet]
        public IActionResult Gracias(EncuestaViewModel model)
        {
            return View(model);
        }
    }
}