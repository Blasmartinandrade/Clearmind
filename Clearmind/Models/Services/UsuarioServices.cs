using System.Linq;
using Microsoft.EntityFrameworkCore;
using Clearmind.Models.Entidades;
using Clearmind.Database;
using Microsoft.VisualBasic;

namespace Clearmind.Models.Services
{
    public class UsuarioServices
    {
        //Inyectamos la clase DataContext mediante el constructor
        private readonly DataContext _context;
        
        public UsuarioServices(DataContext ctx){
            this._context = ctx;
        }
/*----------------------------------------------------------------------------------*/
// Manejamos la bbdd de forma asincronica para evitar bloqueos en el hilo principal 
// y que todo funcione de manera relativamente fluida
/*----------------------------------------------------------------------------------*/

        //Listar todos los usuarios
        public async Task<List<Usuario>> ListaUsuarios()
        {
            
            return await _context.Usuarios.ToListAsync();
            
        }


/*-----------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------*/

//  OPERACIONES BASICAS

/*-----------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------*/

        //Crear usuario

        public async Task<Usuario?> CrearUsuario(Usuario user)
        {
            if (user != null)
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (usuarioExistente != null)
                {
                    return null;
                }

                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }

            return null;
        }

/*-----------------------------------------------------------------------------*/

        //Modificar Usuario

        public async Task<Usuario?> ModificarUsuario(Usuario user){
            if(user != null){
                var u = await _context.Usuarios.FindAsync(user.Id);
                if(u != null){
                    u.setEmail(user.Email);
                    u.setImagen(user.Imagen);
                    u.setNombre(user.Nombre);
                    u.setDescripcion(user.Descripcion);
                    await _context.SaveChangesAsync();

                    return u;
                }
            }
            
            
            return null;

        }

/*----------------------------------------------------------------------------------*/

        //Traer Usuario


        public async Task<Usuario?> TraerUsuarioId(int id){
            var user = await _context.Usuarios.FindAsync(id);
            if(user != null){
                user.Tareas = await ObtenerTareas(user);
                user = await ObtenerProyectos(user);
                return user;

            }
            return null;
        }

        public async Task<Usuario?> TraerUsuarioEmail(string email){  
            var user = await _context.Usuarios.
            Where(u => u.Email == email)
            .FirstOrDefaultAsync();
            
            if(user != null){
                user.Tareas = await ObtenerTareas(user);
                user = await ObtenerProyectos(user);
                return user;

            }
            return null;
        }


/*----------------------------------------------------------------------------------*/

        //Eliminar usuario via Id
        public async Task EliminarUsuario(Usuario user){
            if(user != null){
                var usuario = await _context.Usuarios.FindAsync(user.getId());
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);

                    var tareas = await _context.UsuariosTareas
                    .Where(t => t.UsuarioID == usuario.getId())
                    .ToListAsync();

                    if(tareas != null){
                        for(int i = 0; i<tareas.Count; i++){
                            if(tareas[i] != null){
                                _context.UsuariosTareas.Remove(tareas[i]);
                            }
                        }
                    }



                    await _context.SaveChangesAsync();
                }
            }
            
        }
/*----------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------*/

//  OPERACIONES COMPLEJAS (Manejo de listas)

/*-----------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------*/

        //---------------- USUARIOS & PROYECTOS-------------------//


        //Asignar Proyecto a Usuario
        public async Task<Usuario?> AsignarProyecto(Usuario? user, string token){
            if(user != null){
                var proyecto = await _context.Proyectos
                .Where(p => p.getToken() == token)
                .FirstOrDefaultAsync();
                if(proyecto != null){
                    UsuarioProyecto up = new UsuarioProyecto();
                    up.setIdProyecto(proyecto.getId());
                    up.setIdUsuario(user.getId());

                    await _context.UsuariosProyectos.AddAsync(up);
                    await _context.SaveChangesAsync();

                    user = await TraerUsuarioId(user.getId());
                }
                
                return user;
            }
            
            return null;
        } 

        public async Task<Usuario?> DesasignarProyecto(Usuario? user, Proyecto proyecto){
            if(user != null){
              var up = await _context.UsuariosProyectos
                .Where(up => up.ProyectoID == proyecto.getId() && up.UsuarioID == user.getId())
                .FirstOrDefaultAsync();
                if (up != null)
                {
                    _context.UsuariosProyectos.Remove(up);
                    await _context.SaveChangesAsync();

                    user = await TraerUsuarioId(user.getId());
                }
                
                return user;
            }
            return null;
        }

        //Obtener IDs de proyectos de un usuario via su Id
        public async Task<List<int>> ObtenerIdProyectos(int id){

            //Obtendremos una lista de Id's de proyectos obtenida directamente de la tabla
            //'UsuariosProyectos', todo esto a partir del Id del usuario, equivalente a
            //Select * from UsuariosProyectos where up.UsuarioID == user.Id

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null){
                List<int> proyectosIds = await _context.UsuariosProyectos
                .Where(up => up.UsuarioID == id)
                .Select(up => up.ProyectoID)
                .ToListAsync();

                return proyectosIds;
            }
            else{
                return new List<int>();
            }
            
        }   


        //Recibe los id de proyectos y hace una busqueda en la tabla "Proyectos"
        //para extraer dichos elementos
        
        public async Task<Usuario> ObtenerProyectos(Usuario user){
            List<int> lista = await ObtenerIdProyectos(user.Id);

            for(int i = 0; i<lista.Count; i++){
                Proyecto? p_aux = await _context.Proyectos.FindAsync(lista[i]);
                if(p_aux != null){
                    user.Proyectos.Add(p_aux);
                }
                
            }
            return user;
            
        }

/*-----------------------------------------------------------------------------*/
//-----------------------------------------------------------------------------//

        //---------------- USUARIOS & TAREAS-------------------//


        //Asignar tarea a usuario

        public async Task<Usuario?> AsignarTarea(Usuario? user, Tarea tarea){
            if(user != null){
                UsuarioTarea ut = new UsuarioTarea();
                ut.setIdTarea(tarea.getId());
                ut.setIdUsuario(user.getId());

                await _context.UsuariosTareas.AddAsync(ut);
                user = await TraerUsuarioId(user.getId());
                return user;  
            }
            return null;            
            
        }

        public async Task<Usuario?> DesasignarTarea(Usuario? user, Tarea tarea){
            if(user != null){
              var usuarioTarea = await _context.UsuariosTareas
                .Where(ut => ut.TareaID == tarea.getId() && ut.UsuarioID == user.getId())
                .FirstOrDefaultAsync();
                if (usuarioTarea != null)
                {
                    _context.UsuariosTareas.Remove(usuarioTarea);
                    await _context.SaveChangesAsync();

                    user = await TraerUsuarioId(user.getId());
                }
                
                return user;
            }
            return null;
        }



        //Obtener IDs de Tareas de un usuario via su Id (Se repite el mismo procedimiento que en
        //ObtenerProyecto())
        public async Task<List<int>> ObtenerIdTareas(int id){
            var usuario = await _context.Usuarios.FindAsync(id);
            if(usuario != null){
                List<int> tareasIds = await _context.UsuariosTareas
                .Where(ut => ut.UsuarioID == id)
                .Select(ut => ut.TareaID)
                .ToListAsync();
                
                return tareasIds;
            }
            else{
                return new List<int>();
            }
        }

        public async Task<List<Tarea>> ObtenerTareas(Usuario user){
            List<int> lista = await ObtenerIdTareas(user.Id);

            for(int i = 0; i<lista.Count; i++){
                Tarea? t_aux = await _context.Tareas.FindAsync(lista[i]);
                if(t_aux != null){
                    user.Tareas.Add(t_aux);
                }
                
            }
            return user.Tareas;

        }

/*-----------------------------------------------------------------------------*/






/*------------------------------------------------------------------------------*/
        /*
        public async Task<Usuario> BuscarUsuarioPorCredencialesAsync(string email, string contraseña)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == contraseña);

            if(usuario!=null){
                return usuario;
            }
            else{
                return new Usuario();
            }
            
            
        }
        */

/*-----------------------------------------------------------------------------*/


    }
}