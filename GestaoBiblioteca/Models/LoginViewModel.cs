using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Introduza um email valido")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A palavra-passe é obrigatória")]
        [MinLength(6, ErrorMessage = "A palavra-passe deve ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-passe")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
