using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class Experience
    {

        public Experience()
        {
            this.CVs = new HashSet<CV>();
        }

        public int Id { get; set; }
        //TODO changer en DateDebut et DateFin
        public DateTime DatedeDebut { get; set; }
        public DateTime DatedeFin { get; set; }
        public string Entreprise { get; set; }
        public string Poste { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

        [InverseProperty("Experiences")]
        public Personne Personne { get; set; }

        [ForeignKey("Personne")]
        public int PersonneId { get; set; }

    }
}
