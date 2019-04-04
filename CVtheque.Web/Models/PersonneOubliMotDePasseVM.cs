using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class PersonneOubliMotDePasseVM
    {

        [Required(ErrorMessage = "Ce champ est requis")]
        [EmailAddress]
        [StringLength(70)]
        public string Email { get; set; }

    }
}