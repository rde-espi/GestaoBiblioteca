using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        
        [MaxLength(200)]
        public string Descricao { get; set; }
    }
}
