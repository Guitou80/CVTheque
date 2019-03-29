using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class PersonneVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(30)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(30)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDeNaissance { get; set; }

        public string Photo { get; set; }

        //TODO: limiter en poids et en extensions de fichiers image
        public HttpPostedFileBase FichierPhoto { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [EmailAddress]
        [StringLength(70)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(10)]
        public string NumeroTel { get; set; }

        [Required]
        public bool Permis { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(5)]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Commune { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Ce champ ne correspond pas au champ \"mot de passe\".")]
        public string ConfirmPassword { get; set; }

        //crop image dans formulaire User/Enregistrement
        public int CropX { get; set; }
        public int CropY { get; set; }
        public int CropW { get; set; }
        public int CropH { get; set; }

        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }

        public IEnumerable<CVVM> CVs { get; set; }

    }
}

