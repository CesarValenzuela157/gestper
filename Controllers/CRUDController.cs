using Microsoft.AspNetCore.Mvc;
using Gestper.Data;
using Gestper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestper.Controllers
{
    public class CRUDController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CRUDController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Verificación de sesión
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioCorreo")))
            {
                return RedirectToAction("Login", "Usuario");
            }

            return RedirectToAction("Perfil");
        }
        
        public async Task<IActionResult> Perfil()
        {
            var usuarioCorreo = HttpContext.Session.GetString("UsuarioCorreo");
            if (string.IsNullOrEmpty(usuarioCorreo))
            {
                return RedirectToAction("Login", "Usuario");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == usuarioCorreo);

            if (usuario == null)
            {
                return RedirectToAction("Error", "Home");
            }
            
            return View("crud.perfil", usuario);
        }
        public async Task<IActionResult> TicketsCreados()
        {
            var correo = HttpContext.Session.GetString("UsuarioCorreo");
            if (string.IsNullOrEmpty(correo))
            {
                return RedirectToAction("Login", "Usuario");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tickets = await _context.Tickets
                .Include(t => t.Estado)
                .Include(t => t.Categoria)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .Where(t => t.IdUsuario == usuario.IdUsuario)
                .ToListAsync();

            return View("Views/CRUD/crud.ticket.cshtml", tickets); 
        }

    }
}
