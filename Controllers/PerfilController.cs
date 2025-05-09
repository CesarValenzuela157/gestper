using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestper.Controllers
{
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Perfil/Index
        public async Task<IActionResult> Index()
        {
            var usuarioCorreo = HttpContext.Session.GetString("UsuarioCorreo");

            if (usuarioCorreo != null)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == usuarioCorreo);

                if (usuario != null)
                {
                    return View(usuario); // Muestra la vista de perfil con los datos del usuario
                }
            }

            return RedirectToAction("Login", "Usuario"); // Si no hay usuario en sesión, redirige al login
        }
        public async Task<IActionResult> Editar()
        {
            var usuarioCorreo = HttpContext.Session.GetString("UsuarioCorreo");

            if (usuarioCorreo != null)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == usuarioCorreo);

                if (usuario != null)
                {
                    return View(usuario);
                }
            }

            return RedirectToAction("Login", "Usuario");
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario usuarioEditado)
        {
            ModelState.Remove("Contraseña");

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.IdUsuario == usuarioEditado.IdUsuario);

                    if (usuarioExistente != null)
                    {
                        usuarioExistente.Nombre = usuarioEditado.Nombre;
                        usuarioExistente.Apellido = usuarioEditado.Apellido;
                        usuarioExistente.Correo = usuarioEditado.Correo;
                        usuarioExistente.Telefono = usuarioEditado.Telefono;

                        await _context.SaveChangesAsync();

                        TempData["MensajeExito"] = "Perfil actualizado correctamente.";

                        // ⬇ Redirige a la vista CRUD, no a la pública
                        return RedirectToAction("Perfil", "CRUD");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se encontró el usuario.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar: " + ex.Message);
                }
            }

            return View(usuarioEditado);
        }
    }
}