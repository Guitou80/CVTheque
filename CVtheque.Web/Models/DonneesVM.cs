using CVtheque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVtheque.Web.Models
{
    public class DonneesVM
    {

        public IEnumerable<CompetenceVM> Competences { get; set; }
        public IEnumerable<ExperienceVM> Experiences { get; set; }
        public IEnumerable<FormationVM> Formations { get; set; }
        public IEnumerable<LangueVM> Langues { get; set; }

    }
}