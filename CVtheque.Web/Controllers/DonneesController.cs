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
    public class DonneesController : Controller
    {
        
        [Authorize]
        public ActionResult Index()
        {

            return View();

        }


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


        [HttpGet]
        public ActionResult AddOrEditFormation(int id = 0)
        {

            if (id != 0)
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var forma =
                        (
                            from f in context.Formations
                            where f.Id == id && f.PersonneId == userId
                            select new FormationVM
                            {
                                DateDebut = f.DateDebut,
                                DateFin = f.DateFin,
                                Ecole = f.Ecole,
                                Description = f.Description,
                                Diplome = f.Diplome,
                                Id = id,
                                FormAction = "EditionTraitement",
                                FormTitre = "Edition de cette formation"
                            }).FirstOrDefault();

                    if (forma != null)
                    {
                        return View(forma);
                    }
                    else
                    {
                        return View(new FormationVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle formation" });
                    }
                }

            }

            return View(new FormationVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle formation" });
        }


        [HttpPost]
        public ActionResult AddOrEditFormation(FormationVM formation)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);

            
                if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                        //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                        //It also contains a Enumerable of error messages for each value submitted
                {

                    if (formation.FormAction == "AjoutTraitement")
                    {

                        Formation forma = new Formation();

                        forma.DateDebut = formation.DateDebut;
                        forma.DateFin = formation.DateFin;
                        forma.Ecole = formation.Ecole;
                        forma.Description = formation.Description;
                        forma.Diplome = formation.Diplome;
                        forma.PersonneId = userId;

                        using (Context context = new Context())
                        {
                            context.Formations.Add(forma);
                            context.SaveChanges();
                        }


                    }
                    else if (formation.FormAction == "EditionTraitement")
                    {

                        using (Context context = new Context())
                        {

                            var result = (from f in context.Formations
                                          where f.Id == formation.Id && f.PersonneId == userId
                                          select f).SingleOrDefault();

                            if (result != null)
                            {
                                result.DateDebut = formation.DateDebut;
                                result.DateFin = formation.DateFin;
                                result.Ecole = formation.Ecole;
                                result.Description = formation.Description;
                                result.Diplome = formation.Diplome;

                                context.SaveChanges();
                            }

                        }


                    }

                }
            
            return RedirectToAction("Formations");

        }


        [HttpPost]
        public ActionResult DeleteFormation(IEnumerable<DeleteFormationVM> formations)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int id = -1;

            foreach(DeleteFormationVM form in formations)
            {
                if(form.ToBeDeleted == true)
                {
                    id = form.Id;
                }
            }

            using (Context context = new Context())
            {

                var x = (from f in context.Formations
                         where f.Id == id && f.PersonneId == userId
                         select f).FirstOrDefault();
                if (x != null)
                {
                    context.Formations.Remove(x);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Formations");

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


        [HttpGet]
        public ActionResult AddOrEditCompetence(int id = 0)
        {

            if (id != 0)
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var compet =
                        (
                            from f in context.Competences
                            where f.Id == id && f.PersonneId == userId
                            select new CompetenceVM
                            {
                                Label = f.Label,
                                Details = f.Details,
                                Id = id,
                                FormAction = "EditionTraitement",
                                FormTitre = "Edition de cette compétence"
                            }).FirstOrDefault();

                    if (compet != null)
                    {
                        return View(compet);
                    }
                    else
                    {
                        return View(new CompetenceVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle compétence" });
                    }
                }

            }

            return View(new CompetenceVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle compétence" });
        }


        [HttpPost]
        public ActionResult AddOrEditCompetence(CompetenceVM competence)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);


            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
            {

                if (competence.FormAction == "AjoutTraitement")
                {

                    Competence compet = new Competence();

                    compet.Label = competence.Label;
                    compet.Details = competence.Details;
                    compet.PersonneId = userId;

                    using (Context context = new Context())
                    {
                        context.Competences.Add(compet);
                        context.SaveChanges();
                    }

                }
                else if (competence.FormAction == "EditionTraitement")
                {

                    using (Context context = new Context())
                    {

                        var result = (from c in context.Competences
                                      where c.Id == competence.Id && c.PersonneId == userId
                                      select c).SingleOrDefault();

                        if (result != null)
                        {
                            result.Label = competence.Label;
                            result.Details = competence.Details;

                            context.SaveChanges();
                        }
                    }
                }
            }

            return RedirectToAction("Competences");

        }

        [HttpPost]
        public ActionResult DeleteCompetence(IEnumerable<DeleteCompetenceVM> competences)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int id = -1;

            foreach (DeleteCompetenceVM comp in competences)
            {
                if (comp.ToBeDeleted == true)
                {
                    id = comp.Id;
                }
            }

            using (Context context = new Context())
            {

                var comp = (from c in context.Competences
                         where c.Id == id && c.PersonneId == userId
                         select c).FirstOrDefault();
                if (comp != null)
                {
                    context.Competences.Remove(comp);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Competences");

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


        [HttpGet]
        public ActionResult AddOrEditExperience(int id = 0)
        {

            if (id != 0)
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var exper =
                        (
                            from e in context.Experiences
                            where e.Id == id && e.PersonneId == userId
                            select new ExperienceVM
                            {
                                DateDebut = e.DatedeDebut,
                                DateFin = e.DatedeFin,
                                Poste = e.Poste,
                                Entreprise = e.Entreprise,
                                Description = e.Description,
                                Id = id,
                                FormAction = "EditionTraitement",
                                FormTitre = "Edition de cette expérience"
                            }).FirstOrDefault();

                    if (exper != null)
                    {
                        return View(exper);
                    }
                    else
                    {
                        return View(new ExperienceVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle expérience" });
                    }
                }

            }

            return View(new ExperienceVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle expérience" });
        }


        [HttpPost]
        public ActionResult AddOrEditExperience(ExperienceVM experience)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);


            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
            {

                if (experience.FormAction == "AjoutTraitement")
                {

                    Experience exp = new Experience();

                    exp.DatedeDebut = experience.DateDebut;
                    exp.DatedeFin = experience.DateFin;
                    exp.Poste = experience.Poste;
                    exp.Entreprise = experience.Entreprise;
                    exp.Description = experience.Description;
                    exp.PersonneId = userId;

                    using (Context context = new Context())
                    {
                        context.Experiences.Add(exp);
                        context.SaveChanges();
                    }

                }
                else if (experience.FormAction == "EditionTraitement")
                {

                    using (Context context = new Context())
                    {

                        var result = (from e in context.Experiences
                                      where e.Id == experience.Id && e.PersonneId == userId
                                      select e).SingleOrDefault();

                        if (result != null)
                        {
                            result.DatedeDebut = experience.DateDebut;
                            result.DatedeFin = experience.DateFin;
                            result.Poste = experience.Poste;
                            result.Entreprise = experience.Entreprise;
                            result.Description = experience.Description;

                            context.SaveChanges();
                        }

                    }


                }

            }

            return RedirectToAction("Experiences");

        }

        [HttpPost]
        public ActionResult DeleteExperience(IEnumerable<DeleteExperienceVM> experiences)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int id = -1;

            foreach (DeleteExperienceVM exper in experiences)
            {
                if (exper.ToBeDeleted == true)
                {
                    id = exper.Id;
                }
            }

            using (Context context = new Context())
            {

                var x = (from e in context.Experiences
                         where e.Id == id && e.PersonneId == userId
                         select e).FirstOrDefault();
                if (x != null)
                {
                    context.Experiences.Remove(x);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Experiences");

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


        [HttpGet]
        public ActionResult AddOrEditLangue(int id = 0)
        {

            if (id != 0)
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var lang =
                        (
                            from l in context.Langues
                            where l.Id == id && l.PersonneId == userId
                            select new LangueVM
                            {
                                Label = l.Label,
                                Niveau = l.Niveau,
                                Id = id,
                                FormAction = "EditionTraitement",
                                FormTitre = "Edition de cette langue"
                            }).FirstOrDefault();

                    if (lang != null)
                    {
                        return View(lang);
                    }
                    else
                    {
                        return View(new LangueVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle langue" });
                    }
                }

            }

            return View(new LangueVM { FormAction = "AjoutTraitement", FormTitre = "Nouvelle langue" });
        }


        [HttpPost]
        public ActionResult AddOrEditLangue(LangueVM langue)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);


            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
            {

                if (langue.FormAction == "AjoutTraitement")
                {

                    Langue lang = new Langue();

                    lang.Label = langue.Label;
                    lang.Niveau = langue.Niveau;
                    lang.PersonneId = userId;

                    using (Context context = new Context())
                    {
                        context.Langues.Add(lang);
                        context.SaveChanges();
                    }

                }
                else if (langue.FormAction == "EditionTraitement")
                {

                    using (Context context = new Context())
                    {

                        var result = (from l in context.Langues
                                      where l.Id == langue.Id && l.PersonneId == userId
                                      select l).SingleOrDefault();

                        if (result != null)
                        {

                            result.Label = langue.Label;
                            result.Niveau = langue.Niveau;

                            context.SaveChanges();
                        }

                    }
                }
            }

            return RedirectToAction("Langues");

        }


        [HttpPost]
        public ActionResult DeleteLangue(IEnumerable<DeleteLangueVM> langues)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            int id = -1;

            foreach (DeleteLangueVM lang in langues)
            {
                if (lang.ToBeDeleted == true)
                {
                    id = lang.Id;
                }
            }

            using (Context context = new Context())
            {
                var x = (from l in context.Langues
                         where l.Id == id && l.PersonneId == userId
                         select l).FirstOrDefault();
                if (x != null)
                {
                    context.Langues.Remove(x);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Langues");

        }

    }
}