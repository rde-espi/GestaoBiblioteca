using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoBiblioteca.Data.Entities
{
    public class Livro:IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; }

        [Range(1, int.MaxValue,ErrorMessage ="Introduza um ano de publicação valido")]

        public int AnoPublicacao { get; set; }

        public bool Disponivel {  get; set; }

        [MaxLength(500)]
        public string ImagemCapaUrl { get; set; }
        [NotMapped]
        public string ImagemCapa => string.IsNullOrWhiteSpace(ImagemCapaUrl)
            ? "/images/noimage.png"
            : ImagemCapaUrl;

        [Range(1,int.MaxValue,ErrorMessage = "Selecione uma categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public ICollection<LivroAutor> LivrosAutores { get; set; }
    }
}
