using Clearmind.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Clearmind.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DB SETS
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioProyecto> UsuariosProyectos { get; set; }
        public DbSet<UsuarioTarea> UsuariosTareas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Objetivo> Objetivos { get; set; }



        // Configuraciones del mapeo de tablas basadas en entidades haciendo uso de 'modelBuilder'
        // Clase que contendra varios metodos que nos ayudaran a definir caracteristicas escenciales para el mapeo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


/*-----------------------------------------------------------------------------*/

            //Indicamos que queremos un esquema 'public' default en PostgreSQL,
            //operacion que realizaremos para cada tabla para asegurar el estandar

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Proyecto>().ToTable("Proyectos");

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<UsuarioProyecto>().ToTable("UsuariosProyectos");


            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<UsuarioTarea>().ToTable("UsuariosTareas");

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Tarea>().ToTable("Tareas");

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Objetivo>().ToTable("Objetivos");





/*-----------------------------------------------------------------------------*/

            // Esto indica que la tabla UsuariosProyectos tendra una clave compuesta por 
            // UsuarioID y ProyectoID

            modelBuilder.Entity<UsuarioProyecto>()
            .HasKey(up => new { up.UsuarioID, up.ProyectoID });

            
            modelBuilder.Entity<UsuarioTarea>()
            .HasKey(ut => new { ut.UsuarioID, ut.TareaID});

        }
    }
}