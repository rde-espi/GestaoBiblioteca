using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; }

        public int AnoPublicacao { get; set; }

        public bool Disponivel {  get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public ICollection<LivroAutor> LivrosAutores { get; set; }
    }
}
