using System.Linq;
using Microsoft.EntityFrameworkCore;
using Clearmind.Models.Entidades;
using Clearmind.Database;
using Microsoft.VisualBasic;

namespace Clearmind.Models.Services{

    public class ProyectoServices{
        
        private readonly DataContext _context;
        public ProyectoServices(DataContext _context){
            this._context = _context;
        }


        public async Task<List<Proyecto>> ListaProyectos(){
            return await _context.Proyectos.ToListAsync();
        }


        public async Task<Proyecto?> CrearProyecto(Proyecto? proyecto, Usuario user){
            if(proyecto != null && user != null){
                
                await _context.Proyectos.AddAsync(proyecto);

                //Buscar proyecto creado para encontrar su Id

                proyecto = await _context.Proyectos
                .Where(p => p.getToken() == proyecto.Token)
                .FirstOrDefaultAsync();
                
                if(proyecto != null){
                    UsuarioProyecto up = new UsuarioProyecto();
                    up.setIdProyecto(proyecto.getId());
                    up.setIdUsuario(user.getId());
                    await _context.UsuariosProyectos.AddAsync(up);
                    await _context.SaveChangesAsync();

                }
                else{
                    return null; //Token invalido
                }
                
                return proyecto;
            }

            return null; //Error al cargar proyecto (Proyecto o Usuario no existente)
        }

        public async Task<Proyecto?> RecuperarProyectoId(int id){
            var proyecto = await _context.Proyectos.FindAsync(id);
            if(proyecto != null){

                proyecto = await ObtenerObjetivos(proyecto);

                return proyecto;
            }
            return null;
        
        }


        public async Task EliminarProyecto(Proyecto proyecto){
            if(proyecto !=null){
                _context.Proyectos.Remove(proyecto);
                

                List<UsuarioProyecto> up = await _context.UsuariosProyectos
                .Where(upp => upp.ProyectoID == proyecto.getId())
                .ToListAsync();
                if(up != null){
                    for(int h=0; h<up.Count; h++){
                        if(up[h]!=null){
                            _context.UsuariosProyectos.Remove(up[h]);
                        }
                    }
                    
                }

                List<Objetivo> objetivos = await _context.Objetivos
                .Where(o => o.ProyectoID == proyecto.Id)
                .ToListAsync();
                if(objetivos != null){
                    for(int i = 0; i<objetivos.Count; i++){
                        if(objetivos[i] != null){
                            _context.Objetivos.Remove(objetivos[i]);
                            

                            List<Tarea> tareas = await _context.Tareas
                            .Where(t => t.ObjetivoID == objetivos[i].getId())
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

                

                

                await _context.SaveChangesAsync();
            } 
        }

        

        public async Task<Proyecto?> ObtenerObjetivos(Proyecto? proyecto){
            
            if(proyecto!=null){

                var objetivos = await _context.Objetivos
                .Where(o => o.getProyectoID() == proyecto.getId())
                .ToListAsync();

                for(int i = 0; i<objetivos.Count; i++){
                    if(objetivos[i] != null){
                        objetivos[i].Tareas = await ObtenerTareas(objetivos[i]);
                        proyecto.Objetivos.Add(objetivos[i]);
                        
                    }
                } 

                return proyecto;

            }
            
            return null;

        }


        public async Task<List<Tarea>?> ObtenerTareas(Objetivo o){

            if(o != null){
                
                var tareas = await _context.Tareas
                .Where(t => t.getObjetivoID() == o.getId())
                .ToListAsync();

                for(int i = 0; i<tareas.Count; i++){
                    if(tareas[i] != null){
                        o.Tareas.Add(tareas[i]);
                    }
                }

            }

            return null;
        }


    }



}