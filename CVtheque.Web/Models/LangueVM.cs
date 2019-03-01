using CVtheque.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class LangueVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Label { get; set; }

        [Required(ErrorMessage = "Ce champ est requis.")]
        public Niveau Niveau { get; set; }
 
        public string FormAction { get; set; }

        public string FormTitre { get; set; }

        public IEnumerable<CVVM> CVs { get; set; }

        public LangueVM()
        {
            FormAction = "Ajout";
            FormTitre = "Nouvelle langue";
        }

    }
}