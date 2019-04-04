using CVtheque.Data;
using CVtheque.Entities;
using CVtheque.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace CVtheque.Web.Controllers
{
    public class UserController : Controller
    {
        //Verify Email Link

        [HttpGet]
        public ActionResult VerificationCompte(string id)
        {

            bool status = false;

            using (Context context = new Context())
            {
                var v = context.Personnes.Where(a=>a.ActivationCode == new Guid(id)).FirstOrDefault();
                if(v != null)
                {
                    v.IsEmailVerified = true;
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }

            ViewBag.Status = status;
            return View();
        }
        

        //Register get action
        [HttpGet]
        public ActionResult Enregistrement()
        {

            return View();
 
        }

        //Register post action
        [HttpPost]
        [ValidateAntiForgeryToken] // permet de lutter contre la contrefaçon d'une requete
        public ActionResult Enregistrement([Bind(Exclude = "IsEmailVerified, ActivationCode")] PersonneVM personne)
        {
            bool status = false;
            string message = "";
            Bitmap bmp;
            Graphics g;

            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
                {

                //Email already exists

                var emailExists = EmailExists(personne.Email);
                if (emailExists)
                {
                    //ModelState.AddModelError("EmailExists", "Email already exists");

                    ViewBag.Message = "Cet email existe deja";
                    ViewBag.Status = false;

                    return View(personne);
                }

                //PHOTO

                string fileName = "";

                if (personne.FichierPhoto != null)
                {

                    if (!String.IsNullOrEmpty(personne.FichierPhoto.FileName))
                    {

                        fileName = Path.GetFileNameWithoutExtension(personne.FichierPhoto.FileName);
                        string extension = Path.GetExtension(personne.FichierPhoto.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        personne.Photo = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        personne.FichierPhoto.SaveAs(fileName);

                        Image myImage = Image.FromFile(fileName);

                        if (extension != ".jpg" && extension != ".bmp")
                        {
                            ViewBag.Message = "Le fichier image doit avoir une extension .jpg ou .bmp";
                            ViewBag.Status = false;

                            if (System.IO.File.Exists(fileName))
                            {
                                myImage.Dispose();
                                System.IO.File.Delete(fileName);
                            }

                            return View(personne);
                        }

                        //On recadre l'image

                        if (personne.CropW > 0 && personne.CropH > 0)
                        {
                            bmp = new Bitmap(personne.CropW, personne.CropH, myImage.PixelFormat);
                            g = Graphics.FromImage(bmp);
                            g.DrawImage(myImage, new Rectangle(0, 0, personne.CropW, personne.CropH),
                                new Rectangle(personne.CropX, personne.CropY, personne.CropW, personne.CropH), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            bmp = new Bitmap(myImage.Width, myImage.Height, myImage.PixelFormat);
                            g = Graphics.FromImage(bmp);
                            g.DrawImage(myImage, new Rectangle(0, 0, myImage.Width, myImage.Height),
                                new Rectangle(0, 0, myImage.Width, myImage.Height), GraphicsUnit.Pixel);
                        }

                        if (bmp.Width > 300) // si largeur de l'image supérieure à 300px on la redimmensionne
                        {
                            float ratio = (float)(bmp.Height) / (float)(bmp.Width);
                            int newHeight = (int)(300 * ratio);

                            bmp = ResizeImage(bmp, 300, newHeight);
                        }

                        System.Drawing.Imaging.ImageFormat frm = myImage.RawFormat;
                        myImage.Dispose();
                        bmp.Save(fileName, frm);

                    }
                }

                //Generate activation code

                personne.ActivationCode = Guid.NewGuid();

                //Password hashing

                personne.Password = Crypto.Hash(personne.Password);
                personne.ConfirmPassword = Crypto.Hash(personne.ConfirmPassword);

                personne.IsEmailVerified = false;

                //save date to database

                Personne user = new Personne();

                user.Prenom = personne.Prenom;
                user.Nom = personne.Nom;
                user.Photo = personne.Photo;
                user.DateDeNaissance = personne.DateDeNaissance;
                user.Email = personne.Email;
                user.NumeroTel = personne.NumeroTel;
                user.Permis = personne.Permis;
                user.Adresse = personne.Adresse;
                user.CodePostal = personne.CodePostal;
                user.Commune = personne.Commune;
                user.Password = personne.Password;
                user.IsEmailVerified = personne.IsEmailVerified;
                user.ActivationCode = personne.ActivationCode;

                using (Context context = new Context())
                {
                    context.Personnes.Add(user);
                    context.SaveChanges();

                    //Send email to user

                    //TODO : faire le insert une fois certain que l'email a été envoyé

                     SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                    message = "L'enregistrement s'est déroulé avec succès, toutefois il vous faut cliquer sur ce lien qui vous a été envoyé à " + user.Email + 
                        " pour valider votre compte.";
                    status = true; 

                }

            }
            else
            {

                message = "invalid request";

            }

            ViewBag.Message = message;
            ViewBag.Status = status;

            return View(personne);
        }


        //Login

        [HttpGet]
        public ActionResult Connexion()
        {

            return View();
        }


        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion(PersonneVM personne, string returnUrl)
        {

            string message = "";

            Personne user = new Personne();

            user.Email = personne.Email;
            user.Password = personne.Password;

            using (Context context = new Context())
            {
                var v = context.Personnes.Where(a => a.Email == user.Email && a.IsEmailVerified == true).FirstOrDefault();

                if(v != null)
                {
                    if (string.Compare(Crypto.Hash(personne.Password), v.Password) == 0)
                    {
                        //TODO ajouter le booléen RememberMe aux entities et à la base de données
                        //int timeout = personne.RememberMe ? 525600 : 120;
                        int timeout = 120;

                        var ticket = new FormsAuthenticationTicket(v.Id.ToString(), false, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        message = "Mot de passe Invalide";
                    }
                }
                else
                {
                    message = "Email Invalide";
                }
            }

            ViewBag.Message = message;    

                return View();

        }

        [Authorize]
        [HttpGet]
        public ActionResult Deconnexion()
        {
            return View();
        
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Deconnexion(PersonneVM personne)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Connexion", "User");
        }


        //Verify Email

        [NonAction]
        public bool EmailExists(string email)
        {

            using (Context context = new Context())
            {
                var v = context.Personnes.Where(a=>a.Email == email).FirstOrDefault();
                return v != null;
            }
            
        }

        // Envoi l'email de vérification
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string emailFor = "VerificationCompte")
        {

            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("gferaud80@gmail.com", "CVTheque");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "66617061";
            string subject = "";
            string body = "";

            if(emailFor == "VerificationCompte")
            {
                subject = "Il vous faut activer votre compte";
                body = "<br/> Veuillez svp cliquer sur le lien ci-dessous afin de valider votre compte <br/><br/>" +
                    "<a href='" + link + "'>" + link + "</a>";
            }
            else if(emailFor == "RemplacerMotDePasse")
            {
                subject = "Création d'un nouveau mot de passe";
                body = "<br/> Veuillez svp cliquer sur le lien ci-dessous afin de créer un nouveau mot de passe<br/><br/>" +
                    "<a href='" + link + "'>" + link + "</a>";
            }

            var smtp = new SmtpClient //smtp = Simple Mail Transfer Protocol
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };


            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })smtp.Send(message);

        }


        //Mot de passe oublié ----------------------------

        public ActionResult OubliMotDePasse()
        {

            return View();
        }

        [HttpPost] public ActionResult OubliMotDePasse(PersonneOubliMotDePasseVM persOMDP)
        {

            string message = "";
            bool status = false;

            using (Context context = new Context())
            {

                var pers =
                    ( from p in context.Personnes
                        where p.Email == persOMDP.Email
                        select p).FirstOrDefault();

                if (pers != null)
                {

                    Guid resetCode = Guid.NewGuid();
                    SendVerificationLinkEmail(persOMDP.Email, resetCode.ToString(), "RemplacerMotDePasse");
                    pers.ActivationCode = resetCode;

                    //pour éviter le problème de la confirmation de mot de passe
                    context.Configuration.ValidateOnSaveEnabled = false;

                    message = "Un email vient de vous être envoyé dans lequel est présent lien vous permettant d'activer le renouvellement de votre mot de passe."; ;
                    status = true;

                    context.SaveChanges();
                }
                else
                {

                    message = "Votre email ne correspond à aucun compte utilisateur.";
                    status = false;

                }
            }

            ViewBag.Message = message;
            ViewBag.Status = status;

            return View();
        }


        [HttpGet]
        public ActionResult RemplacerMotDePasse(string id)
        {

            Guid guidActivationCode = new Guid(id);

            using (Context context = new Context())
            {
                var pers =
                    (
                        from p in context.Personnes
                        where p.ActivationCode == guidActivationCode
                        select p).FirstOrDefault();

                if (pers != null)
                { 
                    return View(new PersonneRemplacerMotDePasseVM { ActivationCode = guidActivationCode});
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        
        [HttpPost]
        public ActionResult RemplacerMotDePasse(PersonneRemplacerMotDePasseVM persRMDP)
        {

            string message = "";
            bool status = false;

            if (ModelState.IsValid){

                using (Context context = new Context())
                {

                    var pers =
                        (
                            from p in context.Personnes
                            where p.ActivationCode == persRMDP.ActivationCode
                            select p).FirstOrDefault();

                    if (pers != null)
                    {
                        pers.Password = Crypto.Hash(persRMDP.Password);
                        pers.ActivationCode = Guid.Empty;
                        context.SaveChanges();

                        message = "Votre mot de passe a bien été remplacé. Vous pouvez à présent vous loguer";
                        status = true;
                    }
                    else
                    {
                        message = "Votre mot de passe n'a pas été remplacé.";
                        status = false;
                    }
                }

            }
            else
            {
                message = "ModelState non valide";
                status = false;
            }

            ViewBag.Message = message;
            ViewBag.Status = status;

            return View();

        }


        //--------------------------------------------------------
        // donnees et cvs ----------------------------------------


        [Authorize]
        public ActionResult Profil()
        {

            using (Context context = new Context())
            {

                int userId = int.Parse(HttpContext.User.Identity.Name);

                var query =
                    (
                        from p in context.Personnes
                        where p.Id == userId
                        select new PersonneEditionVM
                        {
                            Prenom = p.Prenom,
                            Nom = p.Nom,
                            DateDeNaissance = p.DateDeNaissance,
                            NumeroTel = p.NumeroTel,
                            Permis = p.Permis,
                            CodePostal = p.CodePostal,
                            Adresse = p.Adresse,
                            Commune = p.Commune,
                            Photo = p.Photo
                        }).FirstOrDefault();

                return View(query);

            }
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken] // permet de lutter contre la contrefaçon d'une requete
        public ActionResult Profil(PersonneEditionVM personne)
        {
            bool status = false;

            string message = "";
            string fileName = "";
            Bitmap bmp;
            Graphics g;

            if (ModelState.IsValid) //Despite its name, it doesn't actually know anything about any model classes. 
                                    //The ModelState represents a Enumerable of name and value pairs that were submitted to the server during a POST. 
                                    //It also contains a Enumerable of error messages for each value submitted
            {
                //PHOTO

                string PhotoRemplacee = Path.Combine(Server.MapPath("~/Images/") + personne.Photo);

                if (personne.FichierPhoto != null)
                {

                    if (!String.IsNullOrEmpty(personne.FichierPhoto.FileName))
                    {

                        fileName = Path.GetFileNameWithoutExtension(personne.FichierPhoto.FileName);
                        string extension = Path.GetExtension(personne.FichierPhoto.FileName);

                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string fileNameFullPath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        personne.FichierPhoto.SaveAs(fileNameFullPath);

                        Image myImage = Image.FromFile(fileNameFullPath);

                        if (extension != ".jpg" && extension != ".bmp")
                        {

                            ViewBag.Message = "Le fichier image doit avoir une extension .jpg ou .bmp";
                            ViewBag.Status = false;

                            if (System.IO.File.Exists(fileNameFullPath))
                            {
                                myImage.Dispose();
                                System.IO.File.Delete(fileNameFullPath);
                            }

                            return View(personne);
                        }


                        personne.Photo = fileName;

                        //On supprimer le précédent fichier image
                        if (System.IO.File.Exists(PhotoRemplacee))
                        {
                            System.IO.File.Delete(PhotoRemplacee);
                        }

                        //On recadre l'image

                        if (personne.CropW > 0 && personne.CropH > 0)
                        {
                            bmp = new Bitmap(personne.CropW, personne.CropH, myImage.PixelFormat);
                            g = Graphics.FromImage(bmp);
                            g.DrawImage(myImage, new Rectangle(0, 0, personne.CropW, personne.CropH),
                                new Rectangle(personne.CropX, personne.CropY, personne.CropW, personne.CropH), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            bmp = new Bitmap(myImage.Width, myImage.Height, myImage.PixelFormat);
                            g = Graphics.FromImage(bmp);
                            g.DrawImage(myImage, new Rectangle(0, 0, myImage.Width, myImage.Height),
                                new Rectangle(0, 0, myImage.Width, myImage.Height), GraphicsUnit.Pixel);
                        }

                        if(bmp.Width > 300) // si largeur de l'image supérieure à 300px on la redimmensionne
                        {
                            float ratio = (float)(bmp.Height) / (float)(bmp.Width);
                            int newHeight = (int)(300 * ratio);

                            bmp = ResizeImage(bmp, 300, newHeight);
                        }

                        System.Drawing.Imaging.ImageFormat frm = myImage.RawFormat;
                        myImage.Dispose();
                        bmp.Save(fileNameFullPath, frm);



                    }

                }
                else
                {
                    if(personne.CropX > 0 || personne.CropY > 0) //S'il y a eu recadrage de la photo avec Jcrop sans upload de photo
                    {
                        if (System.IO.File.Exists(PhotoRemplacee))
                        {

                            Image myImage = Image.FromFile(PhotoRemplacee);
                            fileName = personne.Photo;

                            //On recadre l'image

                            bmp = new Bitmap(personne.CropW, personne.CropH, myImage.PixelFormat);
                            g = Graphics.FromImage(bmp);
                            g.DrawImage(myImage, new Rectangle(0, 0, personne.CropW, personne.CropH),
                                new Rectangle(personne.CropX, personne.CropY, personne.CropW, personne.CropH), GraphicsUnit.Pixel);

                            System.Drawing.Imaging.ImageFormat frm = myImage.RawFormat;
                            myImage.Dispose();
                            bmp.Save(PhotoRemplacee, frm);

                        }
                    }
                }


                int userId = int.Parse(HttpContext.User.Identity.Name);

                using (Context context = new Context())
                {

                    var result = (from p in context.Personnes
                                       where p.Id == userId
                                       select p).SingleOrDefault();

                    result.Prenom = personne.Prenom;
                    result.Nom = personne.Nom;
                    result.DateDeNaissance = personne.DateDeNaissance;
                    result.NumeroTel = personne.NumeroTel;
                    result.Permis = personne.Permis;
                    result.Adresse = personne.Adresse;
                    result.CodePostal = personne.CodePostal;
                    result.Commune = personne.Commune;
                    result.Photo = fileName;

                    context.SaveChanges();

                    status = true;

                }

            }
            else
            {

                message = "invalid request";
                status = false;

            }

            ViewBag.Message = message;
            ViewBag.Status = status;

            return View(personne);
        }



        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }
}