using DataLayer.Models;
using Form115.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Form115.Areas.Admin.Controllers
{
    public class PublipostageController : Controller
    {

        private Form115Entities db = new Form115Entities();

        // GET: Admin/Publipostage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnvoiLettreInfo(LettreInfoViewModel lettreInfoVM) {

            var destinataires = db.Utilisateurs.Select(u => new { u.AspNetUsers.Email, u.Prenom, u.Nom}) ;//.Where(u => u.InscritLettreInfo == 1);

            foreach (var utilisateur in destinataires)
            {
                var mailMessage = new MailMessage("Hermétistes Voyages <info-hermetistes@neggruda.net>", utilisateur.Prenom+" "+utilisateur.Nom+" <"+utilisateur.Email+">");
                var client = new SmtpClient { 
                    Host = "smtp.neggruda.net",
                    Port = 2525,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("formationgtm@neggruda.net", "7VfrdsAw")
                };
                mailMessage.Subject = lettreInfoVM.Objet;
                string corps =  lettreInfoVM.Corps.Replace("\Nom", utilisateur.Nom) ;
             //   mailMessage.Body = ;
                return View() ;
            }
        }
    }
}