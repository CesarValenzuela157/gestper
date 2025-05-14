using Microsoft.AspNetCore.Mvc;

namespace Gestper.Controllers;

public class ComenzarAhoraController : Controller
{
    public IActionResult ComenzarAhora()
    {
        return View();
    }
}