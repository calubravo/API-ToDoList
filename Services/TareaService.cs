using API_ToDoList.Domain.DTO;
using API_ToDoList.Entities;
using API_ToDoList.Repository;
using API_ToDoList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Threading;
using static API_ToDoList.Services.TareaService;

namespace API_ToDoList.Services
{
    public class TareaService : ITareasService
    {
            //Todo lo relacionado a la "tematica" de Actores
            // CRUD
            //Reglas de negocios
            //Hacer modificaciones o select a la bd
            private readonly ToDoContext _context;

            public TareaService (ToDoContext context) //Inyeccion de dependencias
            {
                _context = context;
            }

            public async Task<bool> AddTareaAsync(TareaDTO tarea) //Agregar tarea
            {
            var tareaNueva = new Tarea();
            if (tarea.Titulo != null && tarea.Descripcion != null)
            {
                
                tareaNueva.Titulo = tarea.Titulo;
                tareaNueva.Descripcion = tarea.Descripcion;
                tareaNueva.FechaAlta = DateTime.Now;
                tareaNueva.FechaModificacion = DateTime.Now;
                tareaNueva.Activo = true;

                if (tarea.Estado?.ToLower().Trim() == "pendiente" || tarea.Estado?.ToLower().Trim() == "en curso" || tarea.Estado?.ToLower().Trim() == "finalizada")
                {
                    tareaNueva.Estado = tarea.Estado.ToUpper();
                }
                else return false;
             await _context.Tareas.AddAsync(tareaNueva);
            }
           
                int rows = await _context.SaveChangesAsync();

                return rows > 0;
            }

            public async Task<bool> DeleteTareaAsync(int id)
        {
            var tareaMatch = await _context.Tareas.FirstOrDefaultAsync(f => f.Id == id);

            if (tareaMatch != null) { tareaMatch.Activo = false; }

            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
        public async Task<bool> UpdateTareaAsync(int id, string nuevoEstado)
        {
            var tareaMatch = await _context.Tareas.FirstOrDefaultAsync(f => f.Id == id);

            if (tareaMatch is null) return false;

            if (nuevoEstado != null &&
                (nuevoEstado?.ToLower().Trim() == "pendiente" || nuevoEstado?.ToLower().Trim() == "en curso" || nuevoEstado?.ToLower().Trim() == "finalizada"))
            {
                tareaMatch.Estado = nuevoEstado.ToUpper();
                tareaMatch.FechaModificacion = DateTime.Now;
            }
            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
                   
            public async Task<List<Tarea>> GetAllTareasActivasAsync()
            {
           
                return await _context.Tareas
                    .Where(t => t.Activo)
                    .ToListAsync();
            }
            public async Task<List<Tarea>> GetAllTareasEliminadasAsync()
            {

                return await _context.Tareas
                    .Where(t => !t.Activo)
                    .ToListAsync();
            }



    }
    }

