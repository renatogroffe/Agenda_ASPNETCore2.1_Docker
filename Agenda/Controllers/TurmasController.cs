using System.Linq;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    public class TurmasController : Controller
    {
        private readonly AgendaContext _contexto;

        public TurmasController(AgendaContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /turmas
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contexto.Turmas.Include("Professor").Include("Curso").ToList());
        }

        // GET: /turmas/id
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] int id)
        {
            var turma = _contexto.Turmas.Include("Professor").Include("Curso").FirstOrDefault(t => t.Id == id);
            if(turma != null)
            {
                return Ok(turma);
            }
            else
            {
                return NotFound($"Nenhuma {nameof(Turma)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }

        // POST: /turmas
        [HttpPost]
        public IActionResult Post([FromBody] Turma turma)
        {
            if (ModelState.IsValid)
            {
                _contexto.Turmas.Add(turma);
                _contexto.SaveChanges();
                return Ok(turma);
            }
            else
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage)));
            }
        } 

        // PUT: Turmas
        [HttpPut]
        public IActionResult Put([FromBody] Turma turma)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(turma).State = EntityState.Modified;
                _contexto.SaveChanges();
                return Ok(turma);
            }
            else
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage)));
            }
        }

        // DELETE: Turmas/id
        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            Turma turma = _contexto.Turmas.Find(id);
            if (turma != null)
            {
                _contexto.Turmas.Remove(turma);
                _contexto.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"Nenhuma {nameof(Turma)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }
    }
}
