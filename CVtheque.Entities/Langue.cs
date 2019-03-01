using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class Langue
    {

        public Langue()
        {
            this.CVs = new HashSet<CV>(); //relation many to many
        }

        public int Id { get; set; }
        
        public string Label { get; set; }
        public Niveau Niveau { get; set; }

        public virtual ICollection<CV> CVs { get; set; } //relation many to many

        [InverseProperty("Langues")]
        public Personne Personne { get; set; }

        [ForeignKey("Personne")]
        public int PersonneId { get; set; }
    }
}
