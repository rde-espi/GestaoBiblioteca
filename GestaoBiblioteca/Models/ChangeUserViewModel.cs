using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestaoBiblioteca.Models
{
    public class ChangeUserViewModel
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
        public string Email { get; set; }
    }
}

