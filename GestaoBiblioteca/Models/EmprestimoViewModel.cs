using GestaoBiblioteca.Data.Entities;
using System.Collections.Generic;

namespace GestaoBiblioteca.Models
{
    public class EmprestimoViewModel
    {
        public Emprestimo Emprestimo { get; set; }
        public IEnumerable<Leitor> Leitores { get; set; }
        public IEnumerable<Livro> Livros { get; set; }
    }
}
