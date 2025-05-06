using Gestper.Models;
using Microsoft.EntityFrameworkCore;


namespace Gestper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignacion>().HasNoKey();
        }


        // Aquí defines las tablas de la base de datos.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ticket> Tickets { get; set; }  
        public DbSet<Asignacion> Asignaciones { get; set; }

        
    }
}