namespace Gestper.Models
{
    public class TicketAdmin
    {
        public int Id { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreConsulta { get; set; }
        public string Descripcion { get; set; }
        
        public string AsignadoA { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public string Departamento { get; set; }
    }
}