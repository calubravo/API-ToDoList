using API_ToDoList.Domain.DTO;
using API_ToDoList.Domain.Request;
using API_ToDoList.Entities;
using API_ToDoList.Services;
using API_ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
       
        public class TareaController : ControllerBase
        {

            private ITareasService _tareaService;

            public TareaController(ITareasService tareaService) //Inyeccion de dependencia
            {
                _tareaService = tareaService;
            }


            [HttpGet("Listar Activas")]
            public async Task<IActionResult> GetTareasActivas()
            {
                var result = await _tareaService.GetAllTareasActivasAsync();
                return Ok(result);
            }

            [HttpGet("Listar Eliminadas")]
            public async Task<IActionResult> GetTareasEliminadas()
            {
                var result = await _tareaService.GetAllTareasEliminadasAsync();
                return Ok(result);
            }

            [HttpPost("Agregar Tarea")]
            public async Task<IActionResult> AddTarea([FromBody] TareaDTO request)
            {
                var result = await _tareaService.AddTareaAsync(request);

                if (!result) return BadRequest(new { Message = "No se pudo agregar la tarea" });

                return Created("", new { Message = "Tarea insertada correctamente..." });
            }
            [HttpPut("Modificar Tarea")]
            public async Task<IActionResult> UpdateTarea([FromBody] UpdateTareaRequest request)
            {
                var result = await _tareaService.UpdateTareaAsync(request.Id, request.NuevoEstado);

                if (!result) return BadRequest(new { Message = "No se pudo actualizar la tarea" });

                return Ok(new { Message = "Tarea actualizada correctamente..." });

            }
            [HttpDelete("Eliminar")]
            public async Task<IActionResult> DeleteTarea([FromBody] int Id)
        {
            var result = await _tareaService.DeleteTareaAsync(Id);
            if (!result) return BadRequest(new { Message = "No se pudo eliminar la tarea" });

            return Ok(new { Message = " Tarea eliminada correctamente..." });
        }
        }
}
