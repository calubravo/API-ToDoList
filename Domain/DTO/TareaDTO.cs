﻿namespace API_ToDoList.Domain.DTO
{
    public class TareaDTO
    {
        public string? Estado { get; set; } = "Pendiente";

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
