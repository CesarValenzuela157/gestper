using Microsoft.AspNetCore.Mvc;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class ProductoController : Controller
    {
        // GET: /Producto/
        public IActionResult Index()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Producto 1", Precio = 10.99m, Descripcion = "Producto de ejemplo 1" },
                new Producto { Id = 2, Nombre = "Producto 2", Precio = 20.99m, Descripcion = "Producto de ejemplo 2" }
            };

            return View(productos);
        }

        // GET: /Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Producto/Create
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Aquí podrías guardar el producto en la base de datos en un futuro
                return RedirectToAction("Index");
            }

            return View(producto);
        }
    }
}