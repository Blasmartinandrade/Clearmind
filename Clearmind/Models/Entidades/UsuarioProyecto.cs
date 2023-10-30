using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearmind.Models.Entidades{

    
    public class UsuarioProyecto{


        [Key]
        [Column(Order = 1)]
        [AllowNull]
        public int UsuarioID {get; set;}

        [Key]
        [Column(Order = 2)]
        [AllowNull]
        public int ProyectoID {get; set;}

      
        public void setIdUsuario(int id){
            this.UsuarioID = id;
        }

        public void setIdProyecto(int id){
            this.ProyectoID = id;
        }

        
    }



}