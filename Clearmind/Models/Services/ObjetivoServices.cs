using System.Linq;
using Microsoft.EntityFrameworkCore;
using Clearmind.Models.Entidades;
using Clearmind.Database;
using Microsoft.VisualBasic;

namespace Clearmind.Models.Services{

    public class ObjetivoServices{

        private readonly DataContext _context;

        public ObjetivoServices(DataContext ctx){
            this._context = ctx;
        }


        public async Task CrearObjetivo(Proyecto proyecto, Objetivo? obj){
            if(proyecto != null && obj != null){
                obj.setProyectoID(proyecto.getId());
                await _context.Objetivos.AddAsync(obj);

            }
            
        }

        public async Task EliminarObjetivo(Objetivo obj){
            if(obj != null){
                
                _context.Objetivos.Remove(obj);

                List<Tarea> tareas = await _context.Tareas
                .Where(t => t.ObjetivoID == obj.getId())
                .ToListAsync();
                if(tareas != null){
                    for(int j = 0; j<tareas.Count; j++){
                        if(tareas[j] != null){
                            _context.Tareas.Remove(tareas[j]);

                            List<UsuarioTarea> ut = await _context.UsuariosTareas
                            .Where(utt => utt.TareaID == tareas[j].getId())
                            .ToListAsync();
                            if(ut != null){
                                for(int k = 0; k<ut.Count; k++){
                                    if(ut[k]!=null){
                                        _context.UsuariosTareas.Remove(ut[k]);
                                    }
                                }
                            }


                        }
                                    
                    }
                }

            }

        }
        
    }
}