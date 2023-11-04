using System.Linq;
using Microsoft.EntityFrameworkCore;
using Clearmind.Models.Entidades;
using Clearmind.Database;
using Microsoft.VisualBasic;

namespace Clearmind.Models.Services{

    public class TareaServices{

        private readonly DataContext _context;

        public TareaServices(DataContext ctx){
            this._context = ctx;
        }

        public async Task CrearTarea(Objetivo obj, Tarea tarea){
            if(obj !=null && tarea != null){
                tarea.setObjetivoID(obj.getId());
                await _context.Tareas.AddAsync(tarea);

                await _context.SaveChangesAsync();

            }

        }
        
        public async Task EliminarTarea(Tarea? tarea){
            if(tarea != null){
                tarea = await _context.Tareas.FindAsync(tarea.getId());
                if(tarea != null){
                    _context.Tareas.Remove(tarea);

                    var lista_ut = await _context.UsuariosTareas
                    .Where(ut => ut.TareaID == tarea.getId())
                    .ToListAsync();

                    if(lista_ut != null){
                        for(int i = 0; i<lista_ut.Count; i++){
                            if(lista_ut[i] != null){
                                _context.UsuariosTareas.Remove(lista_ut[i]);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }

            }
        }

        public async Task ModificarTarea(Tarea? tarea){
            if(tarea!=null){
                var t_aux = await _context.Tareas.FindAsync(tarea.getId());
                if(t_aux != null){
                    t_aux = tarea;
                    await _context.SaveChangesAsync();
                }
                


            
            }
        }
    }
}