using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class CompetenceVM
    {

        

        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(100)]
        public string Label { get; set; }
        
        
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        public string FormAction { get; set; }

        public string FormTitre { get; set; }

        public IEnumerable<CVVM> CVs { get; set; }

        public CompetenceVM()
        {
            FormAction = "Ajout";
            FormTitre = "Nouvelle formation";
        }

    }
}