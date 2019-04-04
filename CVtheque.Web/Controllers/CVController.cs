using CVtheque.Data;
using CVtheque.Entities;
using CVtheque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVtheque.Web.Controllers
{
    public class CVController : Controller
    {

        // GET: CV

        [Authorize]
        public ActionResult Index()
        {

            using (Context context = new Context())
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                var query =
                    (
                        from p in context.Personnes
                        where p.Id == userId
                        select new PersonneVM
                        {
                            Prenom = p.Prenom,
                            Nom = p.Nom,
                            DateDeNaissance = p.DateDeNaissance,
                            NumeroTel = p.NumeroTel,
                            Email = p.Email,
                            Permis = p.Permis,
                            CodePostal = p.CodePostal,
                            Adresse = p.Adresse,
                            Commune = p.Commune,
                            Photo = p.Photo
                        }).FirstOrDefault();

                return View(query);
            }

        }

        public ActionResult CVs()
        {
            return View(GetAllCVs());
        }


        IEnumerable<CVVM> GetAllCVs()
        {

            using (Context context = new Context())
            {

                var cvVM = new CVVM();
                int userId = int.Parse(HttpContext.User.Identity.Name);

                var donnees =
                    (

                        from p in context.Personnes
                        where p.Id == userId
                        select new CVsVM
                        {
                             CVs = p.CVs.Select(cv => new CVVM
                            {
                                Id = cv.Id,
                                Titre = cv.Titre
                            }),
                        }

                    ).FirstOrDefault();

                return donnees.CVs.ToList<CVVM>();

            }
        }


        [HttpGet]
        public ActionResult AddOrEditCV(int id = 0)
        {

            if (id != 0)
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var cv =(
                            from c in context.CVs
                            where c.Id == id && c.PersonneId == userId
                            select new CVVM
                            {
                                Titre = c.Titre,
                                MontrerPhoto = c.MontrerPhoto,
                                Layout = c.Layout,
                                Id = id,
                                FormAction = "EditionTraitement",
                                FormTitre = "Edition de ce CV",

                                    Formations = from form in c.Formations
                                                where c.Id == id && c.PersonneId == userId
                                                select new FormationVM
                                                {
                                                    Id = form.Id,
                                                    DateDebut = form.DateDebut,
                                                    DateFin = form.DateFin,
                                                    Ecole = form.Ecole,
                                                    Description = form.Description,
                                                    Diplome = form.Diplome
                                                },

                                    Competences = from comp in c.Competences
                                                where c.Id == id && c.PersonneId == userId
                                                select new CompetenceVM
                                                {
                                                    Id = comp.Id,
                                                    Label = comp.Label,
                                                    Details = comp.Details
                                                },

                                Experiences = from exp in c.Experiences
                                                where c.Id == id && c.PersonneId == userId
                                                select new ExperienceVM
                                                {
                                                    Id = exp.Id,
                                                    DateDebut = exp.DatedeDebut,
                                                    DateFin = exp.DatedeFin,
                                                    Entreprise = exp.Entreprise,
                                                    Poste = exp.Poste,
                                                    Description = exp.Description
                                                },

                                Langues = from lang in c.Langues
                                                where c.Id == id && c.PersonneId == userId
                                                select new LangueVM
                                                {
                                                    Id = lang.Id,
                                                    Label = lang.Label,
                                                    Niveau = lang.Niveau
                                                },

                            }).FirstOrDefault();

                    if (cv!= null)
                    {
                        return View(cv);
                    }
                    else
                    {
                        return View(new CVVM { FormAction = "AjoutTraitement", FormTitre = "Nouveau CV" });
                    }
                }

            }

            return View(new CVVM { FormAction = "AjoutTraitement", FormTitre = "Nouveau CV" });
        }

        [HttpPost]
        public ActionResult AddOrEditCV(CVVM cv)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int cvId = cv.Id;

            IEnumerable<int> formationsIds = cv.FormationsIds;
            IEnumerable<int> competencesIds = cv.CompetencesIds;
            IEnumerable<int> experiencesIds = cv.ExperiencesIds;
            IEnumerable<int> languesIds = cv.LanguesIds;


            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
            {

                if (cv.FormAction == "AjoutTraitement")
                {

                    CV entityCV = new CV();

                    entityCV.Titre = cv.Titre;
                    entityCV.MontrerPhoto = cv.MontrerPhoto;
                    entityCV.PersonneId = userId;

                    using (Context context = new Context())
                    {
                        context.CVs.Add(entityCV);
                        context.SaveChanges();
                        cvId = entityCV.Id;
                    }

                }
                else if (cv.FormAction == "EditionTraitement")
                {

                    using (Context context = new Context())
                    {
                        var result = (from c in context.CVs
                                      where c.Id == cvId && c.PersonneId == userId
                                      select c).SingleOrDefault();

                        if (result != null)
                        {

                            result.Titre = cv.Titre;
                            result.MontrerPhoto = cv.MontrerPhoto;

                            context.SaveChanges();
                        }

                    }
                    
                }


                // many to many add or delete
                //TODO essayer  d'en faire une fonction, mais le pb cest que le context.Formations, context.Competences, etc n'ont pas le même type
                using (Context context = new Context())
                {

                    CV currentCV = (from c in context.CVs
                                      where c.Id == cvId && c.PersonneId == userId
                                      select c).FirstOrDefault();

                    //-----

                    if (formationsIds != null)
                    {
                        var clientSideFormationsIds = (from f in context.Formations
                                                       where formationsIds.Contains(f.Id)
                                                       select f.Id);

                        var serverSideFormationsIds = (from f in currentCV.Formations
                                                       select f.Id);

                        var deletedFormationsIds = serverSideFormationsIds.Except(clientSideFormationsIds).ToList<int>();
                        var addedFormationsIds = clientSideFormationsIds.Except(serverSideFormationsIds).ToList<int>();

                        var deletedFormations = (from f in context.Formations
                                                 where deletedFormationsIds.Contains(f.Id)
                                                 select f);

                        foreach (var formation in deletedFormations)
                        {
                            currentCV.Formations.Remove(formation);
                        }

                        var addedFormations = (from f in context.Formations
                                               where addedFormationsIds.Contains(f.Id)
                                               select f);

                        foreach (var formation in addedFormations)
                        {
                            currentCV.Formations.Add(formation);
                        }

                    }
                    else
                    {
                        currentCV.Formations.Clear();
                    }

                    //-----

                    if (competencesIds != null)
                    {
                        var clientSideCompetencesIds = (from c in context.Competences
                                                       where competencesIds.Contains(c.Id)
                                                       select c.Id);

                        var serverSideCompetencesIds = (from c in currentCV.Competences
                                                       select c.Id);

                        var deletedCompetencesIds = serverSideCompetencesIds.Except(clientSideCompetencesIds).ToList<int>();
                        var addedCompetencesIds = clientSideCompetencesIds.Except(serverSideCompetencesIds).ToList<int>();

                        var deletedCompetences = (from c in context.Competences
                                                 where deletedCompetencesIds.Contains(c.Id)
                                                 select c);

                        foreach (var competence in deletedCompetences)
                        {
                            currentCV.Competences.Remove(competence);
                        }

                        var addedCompetences = (from c in context.Competences
                                               where addedCompetencesIds.Contains(c.Id)
                                               select c);

                        foreach (var comp in addedCompetences)
                        {
                            currentCV.Competences.Add(comp);
                        }

                    }
                    else
                    {
                        currentCV.Competences.Clear();
                    }

                    //-----

                    if (experiencesIds != null)
                    {
                        var clientSideExperiencesIds = (from e in context.Experiences
                                                        where experiencesIds.Contains(e.Id)
                                                        select e.Id);

                        var serverSideExperiencesIds = (from e in currentCV.Experiences
                                                        select e.Id);

                        var deletedExperiencesIds = serverSideExperiencesIds.Except(clientSideExperiencesIds).ToList<int>();
                        var addedExperiencesIds = clientSideExperiencesIds.Except(serverSideExperiencesIds).ToList<int>();

                        var deletedExperiences = (from e in context.Experiences
                                                  where deletedExperiencesIds.Contains(e.Id)
                                                  select e);

                        foreach (var exp in deletedExperiences)
                        {
                            currentCV.Experiences.Remove(exp);
                        }

                        var addedExperiences = (from e in context.Experiences
                                                where addedExperiencesIds.Contains(e.Id)
                                                select e);

                        foreach (var exp in addedExperiences)
                        {
                            currentCV.Experiences.Add(exp);
                        }

                    }
                    else
                    {
                        currentCV.Experiences.Clear();
                    }

                    //-----

                    if (languesIds != null)
                    {
                        var clientSideLanguesIds = (from l in context.Langues
                                                        where languesIds.Contains(l.Id)
                                                        select l.Id);

                        var serverSideLanguesIds = (from l in currentCV.Langues
                                                        select l.Id);

                        var deletedLanguesIds = serverSideLanguesIds.Except(clientSideLanguesIds).ToList<int>();
                        var addedLanguesIds = clientSideLanguesIds.Except(serverSideLanguesIds).ToList<int>();

                        var deletedLangues = (from f in context.Langues
                                                  where deletedLanguesIds.Contains(f.Id)
                                                  select f);

                        foreach (var lang in deletedLangues)
                        {
                            currentCV.Langues.Remove(lang);
                        }

                        var addedLangues = (from l in context.Langues
                                                where addedLanguesIds.Contains(l.Id)
                                                select l);

                        foreach (var lang in addedLangues)
                        {
                            currentCV.Langues.Add(lang);
                        }

                    }
                    else
                    {
                        currentCV.Langues.Clear();
                    }

                    context.SaveChanges();

                }


            }

            return RedirectToAction("CVs");

        }


        [HttpPost]
        public ActionResult DeleteCV(IEnumerable<DeleteCVVM> cvs)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int id = -1;

            foreach (DeleteCVVM cv in cvs)
            {
                if (cv.ToBeDeleted == true)
                {
                    id = cv.Id;
                }
            }

            using (Context context = new Context())
            {

                var x = (from cv in context.CVs
                         where cv.Id == id && cv.PersonneId == userId
                         select cv).FirstOrDefault();
                if (x != null)
                {
                    context.CVs.Remove(x);
                    context.SaveChanges();

                    //TODO supprimer les indexes correspondants des tables de liaisons (relations many to many)
                }
            }

            return RedirectToAction("CVs");

        }

        //___________________________________________________
        //___________________________________________________


        public ActionResult Donnees()
        {
            return View();
        }


        public ActionResult CVLayout1(int id = 0)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {
                
                        var donnees =
                            (

                                from cv in context.CVs
                                where cv.Id == id && cv.PersonneId == userId
                                select new CVVM
                                {

                                    Competences = cv.Competences.Select(comp => new CompetenceVM
                                    {
                                        Id = comp.Id,
                                        Label = comp.Label,
                                        Details = comp.Details
                                    }),

                                    Experiences = cv.Experiences.Select(exp => new ExperienceVM
                                    {
                                        Id = exp.Id,
                                        DateDebut = exp.DatedeDebut,
                                        DateFin = exp.DatedeFin,
                                        Entreprise = exp.Entreprise,
                                        Poste = exp.Poste,
                                        Description = exp.Description

                                    }),

                                    Langues = cv.Langues.Select(lan => new LangueVM
                                    {
                                        Id = lan.Id,
                                        Label = lan.Label,
                                        Niveau = lan.Niveau
                                    }), 

                                    Formations = cv.Formations.Select(form => new FormationVM
                                    {
                                        Id = form.Id,
                                        DateDebut = form.DateDebut,
                                        DateFin = form.DateFin,
                                        Ecole = form.Ecole,
                                        Description = form.Description,
                                        Diplome = form.Diplome

                                    }),

                                }

                            ).FirstOrDefault();

                        return View(donnees);
                    }

        }



        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------DONNEES-----------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------Formations --------------------------------------------------------

        public ActionResult Formations()
        {
            return View(GetAllFormations());
        }


        IEnumerable<FormationVM> GetAllFormations()
        {

            using (Context context = new Context())
            {

                var donneesVM = new DonneesVM();
                int userId = int.Parse(HttpContext.User.Identity.Name);


                var donnees =
                    (

                        from personne in context.Personnes
                        where personne.Id == userId
                        select new DonneesVM
                        {

                            Formations = personne.Formations.Select(form => new FormationVM
                            {
                                Id = form.Id,
                                DateDebut = form.DateDebut,
                                DateFin = form.DateFin,
                                Ecole = form.Ecole,
                                Description = form.Description,
                                Diplome = form.Diplome
                            }),

                        }

                    ).FirstOrDefault();

                return donnees.Formations.ToList<FormationVM>();
            }

        }


        //-------------------------------------------------------------------------------------------------------------------------------
        // COMPETENCES ------------------------------------------------------------------------------------------------------------------

        public ActionResult Competences()
        {
            return View(GetAllCompetences());
        }


        IEnumerable<CompetenceVM> GetAllCompetences()
        {

            using (Context context = new Context())
            {

                var donneesVM = new DonneesVM();
                int userId = int.Parse(HttpContext.User.Identity.Name);

                var donnees =
                    (
                        from personne in context.Personnes
                        where personne.Id == userId
                        select new DonneesVM
                        {

                            Competences = personne.Competences.Select(form => new CompetenceVM
                            {
                                Id = form.Id,
                                Label = form.Label,
                                Details = form.Details
                            }),

                        }

                    ).FirstOrDefault();

                return donnees.Competences.ToList<CompetenceVM>();
            }

        }


        //-------------------------------------------------------------------------------------------------------------------------------
        //------------------------------EXPERIENCES -------------------------------------------------------------------------------------

        public ActionResult Experiences()
        {
            return View(GetAllExperiences());
        }


        IEnumerable<ExperienceVM> GetAllExperiences()
        {

            using (Context context = new Context())
            {

                var donneesVM = new DonneesVM();
                int userId = int.Parse(HttpContext.User.Identity.Name);

                var donnees =
                    (
                        from personne in context.Personnes
                        where personne.Id == userId
                        select new DonneesVM
                        {

                            Experiences = personne.Experiences.Select(form => new ExperienceVM
                            {
                                Id = form.Id,
                                DateDebut = form.DatedeDebut,
                                DateFin = form.DatedeFin,
                                Poste = form.Poste,
                                Entreprise = form.Entreprise,
                                Description = form.Description
                            }),

                        }

                    ).FirstOrDefault();

                return donnees.Experiences.ToList<ExperienceVM>();
            }

        }
        

        //-------------------------------------------------------------------------------------------------------------------------------
        //------------------------------LANGUES-------------------------------------------------------------------------------------

        public ActionResult Langues()
        {
            return View(GetAllLangues());
        }


        IEnumerable<LangueVM> GetAllLangues()
        {

            using (Context context = new Context())
            {

                var donneesVM = new DonneesVM();
                int userId = int.Parse(HttpContext.User.Identity.Name);

                var donnees =
                    (
                        from personne in context.Personnes
                        where personne.Id == userId
                        select new DonneesVM
                        {
                            Langues = personne.Langues.Select(lang => new LangueVM
                            {
                                Id = lang.Id,
                                Label = lang.Label,
                                Niveau = lang.Niveau

                            }),

                        }

                    ).FirstOrDefault();

                return donnees.Langues.ToList<LangueVM>();
            }

        }
    }
}