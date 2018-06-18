using System.Linq;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    public class CursosController : Controller
    {
        private readonly AgendaContext _contexto;

        public CursosController(AgendaContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /cursos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contexto.Cursos.ToList());
        }

        // GET: /cursos/id
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] int id)
        {
            var curso = _contexto.Cursos.Find(id);
            if(curso != null)
            {
                return Ok(curso);
            }
            else
            {
                return NotFound($"Nenhum {nameof(Curso)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }

        // POST: /cursos
        [HttpPost]
        public IActionResult Post([FromBody] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _contexto.Cursos.Add(curso);
                _contexto.SaveChanges();
                return Ok(curso);
            }
            else
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage)));
            }
        } 

        // PUT: Cursos
        [HttpPut]
        public IActionResult Put([FromBody] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(curso).State = EntityState.Modified;
                _contexto.SaveChanges();
                return Ok(curso);
            }
            else
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage)));
            }
        }

        // DELETE: Cursos/id
        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            Curso curso = _contexto.Cursos.Find(id);
            if (curso != null)
            {
                _contexto.Cursos.Remove(curso);
                _contexto.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"Nenhum {nameof(Curso)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }
    }
}
