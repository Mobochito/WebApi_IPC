

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApi.Context;
using CrudApi.DTOs;
using CrudApi.Service;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _service;

        public PerfilController(PerfilService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Lista()
        {
            return Ok(await _service.Lista());
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<ActionResult<PerfilDTO>> Buscar(int id)
        {
            var resultado = await _service.Buscar(id);
            if (resultado == null)
            {
                return NotFound("Perfil no encontrado.");
            }
          
            return Ok(resultado);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult<PerfilDTO>> Guardar(PerfilDTO perfilDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var resultado = await _service.Guardar(perfilDTO);
            if (resultado == null)
            {
                return BadRequest("No se pudo guardar el perfil.");
            }
            return Ok(resultado);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult<PerfilDTO>> Editar(PerfilDTO perfilDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var resultado = await _service.Editar(perfilDTO);
            if (resultado == null)
            {
                return NotFound("Perfil no encontrado.");
            }
            return Ok("Perfil modificado.");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<PerfilDTO>> Eliminar(int id)
        {
            var resultado = await _service.Eliminar(id);
            if (resultado == null)
            {
                return NotFound("Perfil no encontrado.");
            }
            return Ok("Perfil eliminado.");
        }


    }
}
