using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearmind.Models.Entidades{

    	
    public class Proyecto{

/*-----------------------------------------------------------------------------*/


        [Key]
        [Column("Id")]
  
        public int Id {get; set;}
	
	    [Required]
        [Column(TypeName = "nchar(50)")]
        [AllowNull]
        public string Nombre {get; set;}
        
	    [Required]
        [Column(TypeName = "nchar(200)")]
	    [AllowNull]
        public string Descripcion {get; set;}

	    [Required]
        [Column(TypeName = "nchar(120)")]
        [AllowNull]
        public string Imagen {get; set;}

	    [Required]
        [Column(TypeName = "nchar(100)")]
        [AllowNull]
        public string Token {get; set;}


        [NotMapped]
        [AllowNull]
        public List<Objetivo> Objetivos {get; set;}



/*-----------------------------------------------------------------------------*/

        //Tendremos una coleccion de Objetos UsuarioProyecto, y a partir de estos obtendremos
        //las instancias de usuarios que pertenecen a cada proyecto para insertarlos en la lista
        // List<> Usuarios, estas operaciones complejas con base de datos se veran codificadas
        // en el modulo 'Services'(Contiene clases estaticas que ofrecen servicios de operacion con bbdd).
        

        // [AllowNull]
        // public ICollection<UsuarioProyecto> UsuariosProyectos {get; set;}


        // [AllowNull]
        // public List<Usuario> Usuarios {get; set;}

/*-----------------------------------------------------------------------------*/        
        //Setters
	
        public void setId(int Id){
            this.Id = Id;
        }

        public void setNombre(string nombre){
            this.Nombre = nombre;
        }

        public void setDescripcion(string descripcion){
            this.Descripcion = descripcion;
        }

        public void setImagen(string imagen){
            this.Imagen = imagen;
        }

        
        //Pseudo encriptacion de clave unica de proyecto o 'Token' autogenerada

        //  random(0, 9) = x;
        //  nombre = JusticeLeague
        //  token = 7J1u4s2t0i3c3L9e6a4g7u8e

        public void setToken(string nombre){

            Random random = new Random(123);
            
            string t = "";
            for(int i = 0; i<nombre.Length; i++){
                int n = random.Next(0, 9);
                t = t + n + nombre[i];
            }
            this.Token = t;
            
        }

/*-----------------------------------------------------------------------------*/

        //GETTERS

        public int getId(){
            return this.Id;
        }

        public string getNombre(){
            return this.Nombre;
        }

        public string getDescripcion(){
            return this.Descripcion;
        }

        public string getImagen(){
            return this.Imagen;
        }

        public string getToken(){
            return this.Token;
        }
	

    }
}