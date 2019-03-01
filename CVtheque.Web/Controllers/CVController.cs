using CVtheque.Data;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Donnees()
        {
            return View();
        }


        public ActionResult CVLayout1(int id = 0)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

             Context context = new Context();

                using (context)
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




/*________________________________________________________________________
 * _______________________________________________________________________
 * 
 Context context = new Context();

            using (context)
            {

                var donneesVM = new DonneesVM();

                int userId = int.Parse(HttpContext.User.Identity.Name);
               

                    var donnees =
                        (

                            from personne in context.Personnes
                            where personne.Id == userId
                            select new DonneesVM
                            {
                                
                                Competences = personne.Competences.Select(comp => new CompetenceVM
                                {
                                    Id = comp.Id,
                                    Label = comp.Label,
                                    Details = comp.Details
                                }),

                                Experiences = personne.Experiences.Select(exp => new ExperienceVM
                                {
                                    Id = exp.Id,
                                    DatedeDebut = exp.DatedeDebut,
                                    DatedeFin = exp.DatedeFin,
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

                    return View(donnees);
                }

*/
