using Microsoft.AspNetCore.Mvc;
using Gestper.Models;
using Gestper.Data;
using Microsoft.EntityFrameworkCore;

namespace Gestper.Controllers
{
    public class HomeControllerAdmin : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeControllerAdmin(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .OrderByDescending(t => t.FechaCreacion)
                .ToListAsync();

            return View("IndexAdmin", tickets);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null)
                return NotFound();

            return View("DetalleTicket", ticket);
        }
    }
}