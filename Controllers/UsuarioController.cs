using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Gestper.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuario/Registrar
        public IActionResult Registrar()
        {
            return View("Views/registro/registro.layout.cshtml");
        }

        // POST: Usuario/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.IdRol = 3; // Rol usuario normal
                usuario.Activo = true;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View("Views/registro/registro.layout.cshtml", usuario);
        }

        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View("Views/login/login.layout.cshtml", new LoginViewModel());
        }

        // POST: Usuario/Login con LoginViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Views/login/login.layout.cshtml", model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == model.Correo);

            if (usuario != null && usuario.Contrasena == model.Contrasena)
            {
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
                HttpContext.Session.SetString("UsuarioId", usuario.IdUsuario.ToString());
                HttpContext.Session.SetString("UsuarioRol", usuario.IdRol.ToString());

                return usuario.IdRol switch
                {
                    1 => RedirectToAction("Index", "HomeControllerAdmin"),
                    2 => RedirectToAction("Index", "Trabajador"),
                    _ => RedirectToAction("Index", "CRUD"),
                };
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View("Views/login/login.layout.cshtml", model);
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}
