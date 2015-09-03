using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Form115.Infrastructure.Helpers
{
    public static class CommentairesHelper
    {
        public static MvcHtmlString CommentairesDivHelper(this HtmlHelper self, List<Commentaires> commentaires, int? idCommentaire, int cpt)
        {
            // le <div> externe
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("Commentaires");
            var s = new StringBuilder(); // new StringBuilder();

            CommentairesDiv(commentaires, idCommentaire, cpt, s);

            divTag.InnerHtml = s.ToString();//
            return new MvcHtmlString(divTag.ToString());
        }

        public static void CommentairesDiv(List<Commentaires> commentaires, int? idCommentaire, int cpt, StringBuilder s)
        {
            foreach (var com in commentaires.Where(c => c.IdCommentaireReference == idCommentaire).OrderBy(c=>c.DateCommentaire))
            {
               s.Append(CommentaireDiv(com, cpt));
               CommentairesDiv(commentaires, com.IdCommentaire, cpt+1, s);
            }
        }

        public static String CommentaireDiv(Commentaires commentaire, int cpt)
        {
            var db = new Form115Entities();

            // le <div> externe
            var divTag = new TagBuilder("div");
            divTag.AddCssClass(String.Format("col-sm-offset-{0}", cpt));


            // le <div> interne FirstLine (présentation)
            var divFirstLineTag = new TagBuilder("div");

            // le <span> interne titre
            var spanTitreTag = new TagBuilder("span");
            spanTitreTag.InnerHtml = commentaire.Titre;

            // le <span> interne auteur
            var spanAuteurTag = new TagBuilder("span");
            spanAuteurTag.InnerHtml = commentaire.Utilisateurs.Prenom + commentaire.Utilisateurs.Nom;

            // le <span> interne date
            var spanDateTag = new TagBuilder("span");
            spanDateTag.InnerHtml = commentaire.DateCommentaire.ToShortDateString();

            // Contenu du div interne FirstLine (présentation)
            divFirstLineTag.InnerHtml = spanTitreTag.ToString() + spanAuteurTag.ToString() + spanDateTag.ToString(); // 

            // le <p> interne texte
            var pTexteTag = new TagBuilder("p");
            pTexteTag.InnerHtml = commentaire.Commentaire;

            // le <div> pour le lien "Répondre"
            var divLastLineTag = new TagBuilder("div");

            // le <a> lien répondre
            var aRepondreTag = new TagBuilder("a");
            aRepondreTag.MergeAttribute("href", "#");
            aRepondreTag.AddCssClass("LienRepondre");
            aRepondreTag.MergeAttribute("id", String.Format("commentaire-{0}",commentaire.IdCommentaire));
            aRepondreTag.InnerHtml = "Répondre";

            // Contenu du divpour le lien "Répondre"
            divLastLineTag.InnerHtml = aRepondreTag.ToString();

            // Contenu du div externe
            divTag.InnerHtml = divFirstLineTag.ToString() + pTexteTag.ToString() + divLastLineTag.ToString();

            return divTag.ToString();
        }
    }
}