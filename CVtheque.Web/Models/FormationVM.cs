using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class FormationVM
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
        public string Ecole { get; set; }
        
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Diplome { get; set; }

        public string FormAction { get; set; }

        public string FormTitre { get; set; }

        public IEnumerable<CVVM> CVs { get; set; }
        //public int CVId { get; set; }

        public FormationVM()
        {
            FormAction = "Ajout";
            FormTitre = "Nouvelle formation";
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now;
        }

    }
}