using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class CV
    {

        public CV()
        {
            this.Experiences = new HashSet<Experience>();
            this.Formations = new HashSet<Formation>();
            this.Competences = new HashSet<Competence>();
            this.Langues = new HashSet<Langue>();
        }

        public int Id { get; set; }

        public string Titre { get; set; }
        public int Layout { get; set; }
        public bool MontrerPhoto { get; set; }

        public virtual ICollection<Langue> Langues { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Formation> Formations { get; set; }
        public virtual ICollection<Competence> Competences { get; set; }

        [InverseProperty("CVs")]
        public Personne Personne { get; set; }

        [ForeignKey("Personne")]
        public int PersonneId { get; set; }

    }
}
