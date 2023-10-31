using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearmind.Models.Entidades{

    public class Tarea{

        [Key]
        [Column("Id")]
        public int Id {get; set;}

        
        [Required]
        [Column(TypeName = "nchar(300)")]
        [AllowNull]
        public string Nombre {get; set;}

        [Required]
        [Column]
        [AllowNull]
        public bool Estado {get; set;}

        [Required]
        [Column]
        [AllowNull]
        public int ObjetivoID {get; set;}


        /*-----------------------------------------------------------------*/

        //Getters

        public int getId(){
            return this.Id;
        }

        public string getNombre(){
            return this.Nombre;
        }

        public bool getEstado(){
            return this.Estado;
        }

        public int getObjetivoID(){
            return this.ObjetivoID;
        }


        /*-----------------------------------------------------------------*/

        //Setters

        public void setId(int id){
            this.Id = id;
        }

        public void setNombre(string nombre){
            this.Nombre = nombre;
        }

        public void Completar(){
            this.Estado = true;
        }

        public void Descompletar(){
            this.Estado = false;
        }

        public void setObjetivoID(int id){
            this.ObjetivoID = id;
        }
    }



}