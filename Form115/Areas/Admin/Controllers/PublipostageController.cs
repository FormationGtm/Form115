using DataLayer.Models;
using Form115.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace Form115.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PublipostageController : Controller
    {

        private Form115Entities db = new Form115Entities();

        // GET: Admin/Publipostage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnvoiLettreInfo(LettreInfoViewModel lettreInfoVM)
        {

            var destinataires = db.Utilisateurs
                                    .Where(u => (bool)u.InscritLettreInfo == true)
                                    .Select(u => new { u.IdUtilisateur, u.Prenom, u.Nom, u.AspNetUsers.Email });
            foreach (var dest in destinataires)
            {
                string AdresseLienDesinscription = Url.Action("Desinscription", "Publipostage", new { id = dest.IdUtilisateur }, "http");

                var chaineDest = dest.Prenom + " " + dest.Nom + " <" + dest.Email + ">";
                var mailMessage = new MailMessage("Hermétistes Voyages <info-hermetistes@neggruda.net>", chaineDest);
                var client = new SmtpClient
                {
                    Host = "smtp.neggruda.net",
                    Port = 2525,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("formationgtm@neggruda.net", "7VfrdsAw")
                };
                mailMessage.Subject = lettreInfoVM.Objet;
                string corpsMessage = lettreInfoVM.Corps;
                corpsMessage = corpsMessage.Replace("@Nom", dest.Nom);
                corpsMessage = corpsMessage.Replace("@Prenom", dest.Prenom);
                if (lettreInfoVM.AdresseLien != "") {
                    corpsMessage += "\n\n" + lettreInfoVM.AdresseLien;
                }
                corpsMessage += "\n\nPour vous désinscrire de cette lettre d'information, cliquez sur le lien ci-dessous.";
                corpsMessage += "\n"+AdresseLienDesinscription;
                mailMessage.Body = corpsMessage;
                //mailMessage.To.Add();
                client.Send(mailMessage);
            }
            var infoVM = new InfoViewModel
            {
                TitrePage = "Confirmation d'envoi de la lettre d'information",
                ResumeInfoTitre = "Envoi réussi.",
                DetailInfo = "La lettre d'information a été envoyée."
            };
            return View("Info", infoVM);
        }

        public ActionResult Desinscription(int id) {
            var infoVM = new InfoViewModel
            {
                TitrePage = "Désinscription de la lettre d'information d'Hermétistes"
            };
            Utilisateurs u = db.Utilisateurs.Find(id);
            if (u == null)
            {
                infoVM.ResumeInfoTitre = "Erreur";
                infoVM.DetailInfo = "L'utilisateur ayant cherché à se désinscrire n'existe plus.";
            }
            else {
                u.InscritLettreInfo = false;
                try
                {
                    db.SaveChanges();
                    infoVM.ResumeInfoTitre = "Désinscription enregistrée.";
                    infoVM.DetailInfo = u.Prenom + " " + u.Nom + ", vous avez bien été enlevé(e) de notre liste de diffusion.";
                    //infoVM.Ajout = new HtmlElement
                    //{
                    //    InnerHtml = "<p> Si c'est une erreur, vous pouvez vous réinscrire en cliquant <a href='/Admin/Publipostage/Inscription/" + u.IdUtilisateur + "'> ici</a></p>."
                    //};
                    //infoVM.AjoutInfo = "Si c'est une erreur, vous pouvez vous réinscrire en cliquant <a href='/Admin/Publipostage/Inscription/"+u.IdUtilisateur+"'> ici</a>.";
                }
                catch
                {
                    infoVM.ResumeInfoTitre = "Echec de la désinscription.";
                    infoVM.DetailInfo = u.Prenom + " " + u.Nom + ", un problème est survenu lors de votre désinscription de notre liste de diffusion.";
                    infoVM.Ajout = "Nous vous conseillons de réessayer dans quelques instants." ;
                }
            }
            return View("Info", infoVM);
        }

        //public ActionResult Inscription(int id)
        //{
        //    Utilisateurs u = db.Utilisateurs.Find(id);
        //    if (u == null)
        //    {
        //        ViewBag.MessageErreur = "L'utilisateur ayant cherché à s'inscrire n'existe plus.";
        //        return RedirectToAction("Erreur", "Home");
        //    }
        //    u.InscritLettreInfo = true;
        //    try
        //    {
        //        db.SaveChanges();
        //        return View("ConfirmDesinscription");
        //    }
        //    catch
        //    {
        //        return View("EchecDesinscription");
        //    }
        //}

    }
}