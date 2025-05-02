using Microsoft.AspNetCore.Mvc;

namespace Gestper.Controllers
{
    public class PreciosController : Controller
    {
        public IActionResult Precios()
        {
            return View();
        }
    }
}