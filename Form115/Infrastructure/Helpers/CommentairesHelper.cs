using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Form115.Infrastructure.Helpers
{
    public static class CommentairesHelper
    {
        public static MvcHtmlString NewCommentaireFormHelper<TModel, TPropertyTitre, TPropertyCommentaire>(this HtmlHelper<TModel> self, Expression<Func<TModel, TPropertyTitre>> expTitre, Expression<Func<TModel, TPropertyCommentaire>> expCommentaire, int idHotel, int? idCommentaireReference)
        {
            // le <div> externe
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("FormulaireCommentaire");
            divTag.AddCssClass("form-group");

            var labelTitre = self.LabelFor(expTitre);
            var inputTitre = self.TextBoxFor(expTitre, new { @class = "form-control" });
            var labelCommentaire = self.LabelFor(expCommentaire);
            var inputCommentaire = self.TextAreaFor(expCommentaire, new { @class = "form-control" });

            var stringHiddenIdCommentaireReference = "<input type=\"hidden\" name=\"IdCommentaire\" value=\"" + idCommentaireReference + "\"/>";

            var stringButtonSubmit = "<button type=\"submit\" class=\"btn-primary btn-lg\" id=\"PostBtn\">Poster</button>";

            divTag.InnerHtml = labelTitre.ToString() + inputTitre.ToString() + labelCommentaire.ToString() + inputCommentaire.ToString() + stringButtonSubmit;

            using (self.BeginForm("Comment", "Hotel", new { id = idHotel }, FormMethod.Post))
            {
                self.ViewContext.Writer.Write(divTag.ToString());
            }
            return new MvcHtmlString("");
        }

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