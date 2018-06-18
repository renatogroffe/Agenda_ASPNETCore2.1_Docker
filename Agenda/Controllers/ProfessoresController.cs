using System.Linq;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    public class ProfessoresController : Controller
    {
        private readonly AgendaContext _contexto;

        public ProfessoresController(AgendaContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /professores
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contexto.Professores.ToList());
        }

        // GET: /professores/id
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] int id)
        {
            var professor = _contexto.Professores.Find(id);
            if(professor != null)
            {
                return Ok(professor);
            }
            else
            {
                return NotFound($"Nenhum {nameof(Professor)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }

        // POST: /professores
        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Professores.Add(professor);
                _contexto.SaveChanges();
                return Ok(professor);
            }
            else
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage)));
            }
        } 

        // PUT: Professores
        [HttpPut]
        public IActionResult Put([FromBody] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(professor).State = EntityState.Modified;
                _contexto.SaveChanges();
                return Ok(professor);
            }
            else
            {
                return BadRequest(new {
                    message = "Requisição inválida.",
                    error = ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage))
                });
            }
        }

        // DELETE: Professores/id
        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            Professor professor = _contexto.Professores.Find(id);
            if (professor != null)
            {
                _contexto.Professores.Remove(professor);
                _contexto.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"Nenhum {nameof(Professor)} com o {nameof(id)} '{id}' foi encontrado.");
            }
        }
    }
}
