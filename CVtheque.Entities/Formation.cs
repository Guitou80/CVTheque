using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class Formation
    {

        public Formation()
        {
            this.CVs = new HashSet<CV>();
        }

        public int Id { get; set; }

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Ecole { get; set; }
        public string Description { get; set; }
        public string Diplome { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

        [InverseProperty("Formations")]
        public Personne Personne { get; set; }

        [ForeignKey("Personne")]
        public int PersonneId { get; set; }
    }
}
