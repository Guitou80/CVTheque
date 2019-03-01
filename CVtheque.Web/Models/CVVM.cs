using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class CVVM
    {

        public int Id { get; set; }

        public string Titre { get; set; }

        //Propriété de navigation
        public PersonneVM Personne { get; set; }
        //Clé étrangère
        public int PersonneId { get; set; }

        public IEnumerable<LangueVM> Langues { get; set; }
        public IEnumerable<ExperienceVM> Experiences { get; set; }
        public IEnumerable<FormationVM> Formations { get; set; }
        public IEnumerable<CompetenceVM> Competences { get; set; }

    }
}