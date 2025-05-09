using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Gestper.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Gestper.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["LoginError"] = "Usuario y contraseña son requeridos.";
                return RedirectToAction("Index", "Home");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == username && u.Contrasena == password && u.Activo);

            if (usuario != null)
            {
                // Guardar datos en sesión
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);

                TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                TempData["LoginError"] = "Usuario o contraseña incorrectos.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string name, string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                TempData["RegisterError"] = "Las contraseñas no coinciden.";
                return RedirectToAction("Index", "Home");
            }

            // Lógica de registro simulada (debes reemplazar con tu lógica real)
            TempData["SuccessMessage"] = "Registro exitoso. Ahora puede iniciar sesión.";
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Limpiar sesión
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Index", "Home");
        }
    }
}
