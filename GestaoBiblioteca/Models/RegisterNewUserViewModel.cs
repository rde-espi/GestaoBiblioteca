using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoBiblioteca.Models
{
    public class RegisterNewUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O apelido é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Apelido")]
        public string LastName { get; set; }

        [MaxLength(150)]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Introduza um número de telefone válido.")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um email válido.")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
        [MinLength(6, ErrorMessage = "A palavra-passe deve ter pelo menos 6 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-passe")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme a palavra-passe.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As palavras-passe não coincidem.")]
        [Display(Name = "Confirmar palavra-passe")]
        public string Confirm { get; set; }
    }
}
