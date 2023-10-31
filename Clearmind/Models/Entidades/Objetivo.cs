using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clearmind.Models.Entidades{

    public class Objetivo{

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
        public int Estado {get; set;}

        [Required]
        [Column]
        [AllowNull]
        public int ProyectoID {get; set;}

        [NotMapped]
        [AllowNull]
        public List<Tarea> Tareas {get; set;}


        /*-----------------------------------------------------------------*/

        //Getters

        public int getId(){
            return this.Id;
        }

        public string getNombre(){
            return this.Nombre;
        }

        public int getEstado(){
            return this.Estado;
        }

        public int getProyectoID(){
            return this.ProyectoID;
        }


        /*-----------------------------------------------------------------*/

        //Setters

        public void setId(int id){
            this.Id = id;
        }

        public void setNombre(string nombre){
            this.Nombre = nombre;
        }

        public void setEstado(){

            if(this.Tareas.Count() == 0){
                this.Estado = 0;
            }
            else{
                int total = 0;
                int completas = 0;
                int incompletas = 0;
                foreach(Tarea t in this.Tareas){
                    total++;
                    if(t.Estado == true){
                        completas++;
                    }
                    else{
                        incompletas++;
                    }
                    
                }

                completas = completas * 100;
                completas = completas / total;

                this.Estado = completas;

            }
            
        }


        public void setProyectoID(int id){
            this.ProyectoID = id;
        }

    }



}