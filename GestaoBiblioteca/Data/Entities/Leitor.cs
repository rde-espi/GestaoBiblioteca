using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Data.Entities
{
    public class Leitor : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
