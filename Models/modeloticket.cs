using System.ComponentModel.DataAnnotations;

namespace Gestper.Models
{
    public class Ticket
    {
        [Key] public int IdTicket { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }

        public int IdCategoria { get; set; }
        public int IdPrioridad { get; set; }
        public int IdDepartamento { get; set; }
        
        


    }
}
