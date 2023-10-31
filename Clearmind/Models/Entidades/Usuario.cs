using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearmind.Models.Entidades{

    	
    public class Usuario{

        [Key]
        [Column("Id")]
  
        public int Id {get; set;}
	
	    [Required]
        [Column(TypeName = "nchar(50)")]
        [AllowNull]
        public string Nombre {get; set;}


        [Required]
        [Column(TypeName = "nchar(250)")]
        [AllowNull]
        public string Descripcion {get; set;}

        [Required]
        [Column(TypeName = "nchar(100)")]
        [AllowNull]
        public string Email {get; set;}
        
	    [Required]
        [Column(TypeName = "nchar(270)")]
	    [AllowNull]
        public string Password {get; set;}

	    [Required]
        [Column(TypeName = "nchar(120)")]
        [AllowNull]
        public string Imagen {get; set;}


/*-----------------------------------------------------------------------------*/

        [AllowNull]
        [NotMapped]
        public List<Proyecto> Proyectos {get; set;}

        [AllowNull]
        [NotMapped]
        public List<Proyecto> Tareas {get; set;}



/*-----------------------------------------------------------------------------*/
        //Setters
	
        public void setId(int Id){
            this.Id = Id;
        }

        public void setNombre(string Nombre){
            this.Nombre = Nombre;
        }

        public void setImagen(string imagen){
            this.Imagen = imagen;
        }

        public void setPassword(string cadena){

            Random random = new Random(123);
            
            string t = "";
            for(int i = 0; i<cadena.Length; i++){
                int n = random.Next(0, 9);
                t = t + n + cadena[i];
            }
            this.Password = t;
            
        }

        public void setEmail(string Email){
            this.Email = Email;
        }

        public void setDescripcion(string Desc){
            this.Descripcion = Desc;
        }

/*-----------------------------------------------------------------------------*/
        //GETTERS

        public int getId(){
            return this.Id;
        }

        public string getNombre(){
            return this.Nombre;
        }

        public string getPassword(){
            return this.Password;
        }

        public string getImagen(){
            return this.Imagen;
        }

        public List<Proyecto> getListProyectos(){
            
            return new List<Proyecto>();

        }

        public string getEmail(){
            return this.Email;
        }

        public string getDescripcion(){
            return this.Descripcion;
        }
    }
}