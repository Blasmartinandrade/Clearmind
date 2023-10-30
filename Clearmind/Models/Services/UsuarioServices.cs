using System.Linq;
using Microsoft.EntityFrameworkCore;
using Clearmind.Models.Entidades;
using Clearmind.Database;

namespace Clearmind.Services
{
    public class UsuarioServices
    {
        public static List<int> ObtenerProyectos(Usuario user, DataContext ctx)
        {
            //Obtendremos una lista de Id's de proyectos obtenida directamente de la tabla
            //'UsuariosProyectos', todo esto a partir del Id del usuario, equivalente a
            //Select * from UsuariosProyectos where up.UsuarioID == user.id

            List<int> proyectosIds = ctx.UsuariosProyectos
                .Where(up => up.UsuarioID == user.Id)
                .Select(up => up.ProyectoID)
                .ToList();

            return proyectosIds;
        }




    }
}