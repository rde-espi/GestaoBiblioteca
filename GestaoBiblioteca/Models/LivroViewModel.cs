using GestaoBiblioteca.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class LivroViewModel
    {
        public Livro Livro { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public IEnumerable<Autor> Autores { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos um autor")]
        [MinLength(1, ErrorMessage = "Selecione pelo menos um autor")]
        public List<int> AutoresSelecionados { get; set; }
    }
}
