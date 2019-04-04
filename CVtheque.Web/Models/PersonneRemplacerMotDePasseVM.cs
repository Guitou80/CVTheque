using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class PersonneRemplacerMotDePasseVM
    {

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(12, MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(12, MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Ce champ ne correspond pas au champ \"mot de passe\".")]
        public string ConfirmPassword { get; set; }

        [Required]
        public System.Guid ActivationCode { get; set; }

    }
}