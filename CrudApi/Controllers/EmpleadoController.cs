using CrudApi.Context;
using CrudApi.DTOs;
using CrudApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApi.Service;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        public readonly EmpleadoService _service;

        public EmpleadoController(EmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Lista()
        {
            return Ok(await _service.Lista());
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<EmpleadoDTO>> Buscar(int id)
        {
            var resultado = await _service.Buscar(id);
            if (resultado is null)
            {
                return NotFound("Empleado no encontrado.");
            }
            return Ok(resultado);
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar(EmpleadoDTO empleadoDTO)
        {
            var resultado = await _service.Guardar(empleadoDTO);
            if (resultado == null)
            {
                return BadRequest("No se pudo guardar el empleado.");
            }
            return Ok(resultado);

        }


        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult<EmpleadoDTO>> Editar(EmpleadoDTO empleadoDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var resultado = await _service.Editar(empleadoDTO);
            if (resultado == null)
            {
                return NotFound("Empleado no encontrado.");
            }
            return Ok("Empleado modificado..."); 
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Eliminar(int id)
        {
            var resultado = await _service.Eliminar(id);
            if (resultado == null)
            {
                return NotFound("Empleado no encontrado.");
            }
            return Ok("Empleado eliminado.");
        }
    }
}
