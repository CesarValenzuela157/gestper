using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Gestper.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrabajadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // AGREGADO: Se añade el parámetro "fechaBusqueda"
        public async Task<IActionResult> Index(int? idDepartamento, int? idEstado, int? idPrioridad, int? idBusqueda, DateTime? fechaBusqueda)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "2")
                return RedirectToAction("Login", "Usuario");

            int idUsuario = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            var ticketsQuery = _context.Tickets
                .Include(t => t.Usuario)
                .Include(t => t.Prioridad)
                .Where(t => t.IdSoporteAsignado == idUsuario)
                .AsQueryable();

            if (idDepartamento.HasValue)
                ticketsQuery = ticketsQuery.Where(t => t.IdDepartamento == idDepartamento);

            if (idEstado.HasValue)
                ticketsQuery = ticketsQuery.Where(t => t.IdEstado == idEstado);

            if (idPrioridad.HasValue)
                ticketsQuery = ticketsQuery.Where(t => t.IdPrioridad == idPrioridad);

            if (idBusqueda.HasValue)
                ticketsQuery = ticketsQuery.Where(t => t.IdTicket == idBusqueda);

            // AGREGADO: filtro por fecha exacta
            if (fechaBusqueda.HasValue)
                ticketsQuery = ticketsQuery.Where(t => t.FechaCreacion.Date == fechaBusqueda.Value.Date);

            var tickets = await ticketsQuery.OrderByDescending(t => t.FechaCreacion).ToListAsync();

            ViewBag.Total = tickets.Count;
            ViewBag.Nuevos = tickets.Count(t => t.IdEstado == 1);
            ViewBag.EnProgreso = tickets.Count(t => t.IdEstado == 2);
            ViewBag.Cerrados = tickets.Count(t => t.IdEstado == 3);

            ViewBag.Departamentos = await _context.Departamentos.ToListAsync();
            ViewBag.Estados = await _context.Estados.ToListAsync();
            ViewBag.Prioridades = await _context.Prioridades.ToListAsync();

            var usuario = await _context.Usuarios
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            ViewBag.DepartamentoNombre = usuario?.Departamento?.Nombre ?? "Sin departamento";

            return View("~/Views/Home/Index_Trabajador.cshtml", tickets);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Usuario)
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.SoporteAsignado)
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null)
                return NotFound();

            var trabajadores = await _context.Usuarios
                .Where(u => u.IdRol == 2)
                .ToListAsync();

            ViewBag.Trabajadores = trabajadores;
            ViewBag.Estados = await _context.Estados.ToListAsync();

            return View("~/Views/Home/DetalleTicket.cshtml", ticket);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Ticket ticket)
        {
            var ticketExistente = await _context.Tickets.FindAsync(ticket.IdTicket);

            if (ticketExistente == null)
                return NotFound();

            ticketExistente.Titulo = ticket.Titulo;
            ticketExistente.Descripcion = ticket.Descripcion;
            ticketExistente.IdPrioridad = ticket.IdPrioridad;
            ticketExistente.IdSoporteAsignado = ticket.IdSoporteAsignado;
            ticketExistente.IdEstado = ticket.IdEstado;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Ticket actualizado correctamente";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}
