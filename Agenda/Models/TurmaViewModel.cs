using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.Models
{
    public class TurmaViewModel
    {
        public Turma Turma { get; set; }
        public IEnumerable<SelectListItem> Cursos { get; set; }
        public IEnumerable<SelectListItem> Professores { get; set; }

        public TurmaViewModel(Turma turma, IEnumerable<Curso> cursos, IEnumerable<Professor> professores)
        {
            Turma = turma;
            Cursos = cursos.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome });
            Professores = professores.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome });
        }
    }
}