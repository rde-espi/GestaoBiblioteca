using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Data.Entities
{
    public class Autor:IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Nacionalidade { get; set; }

        public ICollection<LivroAutor> LivrosAutores { get; set; }
    }
}
