using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class Competence
    {

        public Competence()
        {
            this.CVs = new HashSet<CV>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public string Details { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

        [InverseProperty("Competences")]
        public Personne Personne { get; set; }

        [ForeignKey("Personne")]
        public int PersonneId { get; set; }

    }
}






