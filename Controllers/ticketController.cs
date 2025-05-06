using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Gestper.Models;

namespace Gestper.Controllers
{
    public class TicketController : Controller
    {
        private readonly IConfiguration _config;

        public TicketController(IConfiguration config)
        {
            _config = config;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View("Views/tickets/tickets.cshtml");
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View("Views/tickets/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
                return View(ticket);

            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(@"
                INSERT INTO Tickets (Titulo, Descripcion, FechaCreacion, IdUsuario, IdEstado, IdCategoria, IdPrioridad, IdDepartamento)
                VALUES (@Titulo, @Descripcion, @FechaCreacion, @IdUsuario, @IdEstado, @IdCategoria, @IdPrioridad, @IdDepartamento)", conn);

            cmd.Parameters.AddWithValue("@Titulo", ticket.Titulo);
            cmd.Parameters.AddWithValue("@Descripcion", ticket.Descripcion);
            cmd.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
            cmd.Parameters.AddWithValue("@IdUsuario", ticket.IdUsuario);
            cmd.Parameters.AddWithValue("@IdEstado", ticket.IdEstado);
            cmd.Parameters.AddWithValue("@IdCategoria", ticket.IdCategoria);
            cmd.Parameters.AddWithValue("@IdPrioridad", ticket.IdPrioridad);
            cmd.Parameters.AddWithValue("@IdDepartamento", ticket.IdDepartamento);

            conn.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = new List<Ticket>();
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Tickets", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tickets.Add(new Ticket
                {
                    IdTicket = (int)reader["IdTicket"],
                    Titulo = reader["Titulo"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    FechaCreacion = (DateTime)reader["FechaCreacion"],
                    IdUsuario = reader["IdUsuario"] != DBNull.Value ? (int)reader["IdUsuario"] : 0,
                    IdEstado = reader["IdEstado"] != DBNull.Value ? (int)reader["IdEstado"] : 0,
                    IdCategoria = reader["IdCategoria"] != DBNull.Value ? (int)reader["IdCategoria"] : 0,
                    IdPrioridad = reader["IdPrioridad"] != DBNull.Value ? (int)reader["IdPrioridad"] : 0,
                    IdDepartamento = reader["IdDepartamento"] != DBNull.Value ? (int)reader["IdDepartamento"] : 0
                });
            }

            return View("Views/Tickets/tickets.cshtml", tickets);
        }

    }
}

