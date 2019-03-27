using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class CVVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [StringLength(200)]
        public string Titre { get; set; }
        public int Layout { get; set; }
        public bool MontrerPhoto { get; set; }
        
        //Propriété de navigation
        public PersonneVM Personne { get; set; }
        //Clé étrangère
        public int PersonneId { get; set; }

        public IEnumerable<LangueVM> Langues { get; set; }
        public IEnumerable<ExperienceVM> Experiences { get; set; }
        public IEnumerable<FormationVM> Formations { get; set; }
        public IEnumerable<CompetenceVM> Competences { get; set; }

        public IEnumerable<int> FormationsIds { get; set; }
        public IEnumerable<int> CompetencesIds { get; set; }
        public IEnumerable<int> LanguesIds { get; set; }
        public IEnumerable<int> ExperiencesIds { get; set; }

        public string FormAction { get; set; }

        public string FormTitre { get; set; }

        //public IEnumerable<CVVM> CVs { get; set; }

        public CVVM()
        {
            FormAction = "Ajout";
            FormTitre = "Nouveau CV";
        }

    }
}