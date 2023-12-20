using API_ToDoList.Domain.DTO;
using API_ToDoList.Entities;

namespace API_ToDoList.Services.Interfaces
{
    public interface ITareasService
    {
        public  Task<bool> AddTareaAsync(TareaDTO tarea);
        public  Task<bool> UpdateTareaAsync(int id, string nuevoEstado);
        public  Task<List<Tarea>> GetAllTareasActivasAsync();
        public Task<bool> DeleteTareaAsync(int id);
        public Task<List<Tarea>> GetAllTareasEliminadasAsync();

    }
}
