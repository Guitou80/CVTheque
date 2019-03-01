using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Entities
{
    public class Personne
    {

        public int Id { get; set; }

        public string Prenom { get; set; }
        public string Nom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Email { get; set; }
        public string NumeroTel { get; set; }
        public string Photo{ get; set; }
        public bool Permis { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Commune { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }


        public ICollection<Langue> Langues { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Formation> Formations { get; set; }
        public ICollection<Competence> Competences { get; set; }
        public ICollection<CV> CVs { get; set; }

    }
}
