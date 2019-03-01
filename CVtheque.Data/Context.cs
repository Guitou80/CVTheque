using CVtheque.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVtheque.Data
{
    public class Context : DbContext
    {

         public Context(): base("EFConstring")
        {

        } 

        public DbSet<Personne> Personnes { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Langue> Langues { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Competence> Competences { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personne>()
                .HasMany(p => p.CVs)
                .WithRequired(cv => cv.Personne)
                .WillCascadeOnDelete(false);
        }

    }
}


