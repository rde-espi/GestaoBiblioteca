using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Data.Entities
{
    public class Categoria : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }
    }
}
