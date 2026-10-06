using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class Emprestimo:IEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime DataEmprestimo { get; set; }

        [Required]
        public DateTime DataPrevistaDevolucao { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public int LeitorId { get; set; }
        public Leitor Leitor { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }

    }
}
