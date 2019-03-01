using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class ExperienceVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFin { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Entreprise { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Poste { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string FormAction { get; set; }

        public string FormTitre { get; set; }

        public IEnumerable<CVVM> CVs { get; set; }

        public ExperienceVM()
        {
            FormAction = "Ajout";
            FormTitre = "Nouvelle formation";
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now;
        }
    }
}