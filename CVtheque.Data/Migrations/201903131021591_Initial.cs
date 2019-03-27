namespace CVtheque.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Details = c.String(),
                        PersonneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.PersonneId, cascadeDelete: true)
                .Index(t => t.PersonneId);
            
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        Layout = c.Int(nullable: false),
                        MontrerPhoto = c.Boolean(nullable: false),
                        PersonneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.PersonneId)
                .Index(t => t.PersonneId);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatedeDebut = c.DateTime(nullable: false),
                        DatedeFin = c.DateTime(nullable: false),
                        Entreprise = c.String(),
                        Poste = c.String(),
                        Description = c.String(),
                        PersonneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.PersonneId, cascadeDelete: true)
                .Index(t => t.PersonneId);
            
            CreateTable(
                "dbo.Personnes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prenom = c.String(),
                        Nom = c.String(),
                        DateDeNaissance = c.DateTime(nullable: false),
                        Email = c.String(),
                        NumeroTel = c.String(),
                        Photo = c.String(),
                        Permis = c.Boolean(nullable: false),
                        Adresse = c.String(),
                        CodePostal = c.String(),
                        Commune = c.String(),
                        Password = c.String(),
                        IsEmailVerified = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        Ecole = c.String(),
                        Description = c.String(),
                        Diplome = c.String(),
                        PersonneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.PersonneId, cascadeDelete: true)
                .Index(t => t.PersonneId);
            
            CreateTable(
                "dbo.Langues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Niveau = c.Int(nullable: false),
                        PersonneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.PersonneId, cascadeDelete: true)
                .Index(t => t.PersonneId);
            
            CreateTable(
                "dbo.CVCompetences",
                c => new
                    {
                        CV_Id = c.Int(nullable: false),
                        Competence_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CV_Id, t.Competence_Id })
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .ForeignKey("dbo.Competences", t => t.Competence_Id, cascadeDelete: true)
                .Index(t => t.CV_Id)
                .Index(t => t.Competence_Id);
            
            CreateTable(
                "dbo.ExperienceCVs",
                c => new
                    {
                        Experience_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Experience_Id, t.CV_Id })
                .ForeignKey("dbo.Experiences", t => t.Experience_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.Experience_Id)
                .Index(t => t.CV_Id);
            
            CreateTable(
                "dbo.FormationCVs",
                c => new
                    {
                        Formation_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Formation_Id, t.CV_Id })
                .ForeignKey("dbo.Formations", t => t.Formation_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.Formation_Id)
                .Index(t => t.CV_Id);
            
            CreateTable(
                "dbo.LangueCVs",
                c => new
                    {
                        Langue_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Langue_Id, t.CV_Id })
                .ForeignKey("dbo.Langues", t => t.Langue_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.Langue_Id)
                .Index(t => t.CV_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Competences", "PersonneId", "dbo.Personnes");
            DropForeignKey("dbo.CVs", "PersonneId", "dbo.Personnes");
            DropForeignKey("dbo.Experiences", "PersonneId", "dbo.Personnes");
            DropForeignKey("dbo.Langues", "PersonneId", "dbo.Personnes");
            DropForeignKey("dbo.LangueCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.LangueCVs", "Langue_Id", "dbo.Langues");
            DropForeignKey("dbo.Formations", "PersonneId", "dbo.Personnes");
            DropForeignKey("dbo.FormationCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.FormationCVs", "Formation_Id", "dbo.Formations");
            DropForeignKey("dbo.ExperienceCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.ExperienceCVs", "Experience_Id", "dbo.Experiences");
            DropForeignKey("dbo.CVCompetences", "Competence_Id", "dbo.Competences");
            DropForeignKey("dbo.CVCompetences", "CV_Id", "dbo.CVs");
            DropIndex("dbo.LangueCVs", new[] { "CV_Id" });
            DropIndex("dbo.LangueCVs", new[] { "Langue_Id" });
            DropIndex("dbo.FormationCVs", new[] { "CV_Id" });
            DropIndex("dbo.FormationCVs", new[] { "Formation_Id" });
            DropIndex("dbo.ExperienceCVs", new[] { "CV_Id" });
            DropIndex("dbo.ExperienceCVs", new[] { "Experience_Id" });
            DropIndex("dbo.CVCompetences", new[] { "Competence_Id" });
            DropIndex("dbo.CVCompetences", new[] { "CV_Id" });
            DropIndex("dbo.Langues", new[] { "PersonneId" });
            DropIndex("dbo.Formations", new[] { "PersonneId" });
            DropIndex("dbo.Experiences", new[] { "PersonneId" });
            DropIndex("dbo.CVs", new[] { "PersonneId" });
            DropIndex("dbo.Competences", new[] { "PersonneId" });
            DropTable("dbo.LangueCVs");
            DropTable("dbo.FormationCVs");
            DropTable("dbo.ExperienceCVs");
            DropTable("dbo.CVCompetences");
            DropTable("dbo.Langues");
            DropTable("dbo.Formations");
            DropTable("dbo.Personnes");
            DropTable("dbo.Experiences");
            DropTable("dbo.CVs");
            DropTable("dbo.Competences");
        }
    }
}
