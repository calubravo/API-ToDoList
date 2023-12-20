using API_ToDoList.Domain.DTO;

namespace API_ToDoList.Domain.Request
{
    public class UpdateTareaRequest
    {
        public int Id { get; set; }
        public string NuevoEstado { get; set; }
    }
}
