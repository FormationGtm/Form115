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
          

            var stringButtonSubmit = "<button type=\"submit\" class=\"btn-info btn-lg\" id=\"PostBtn\">Poster</button>";

            divTag.InnerHtml = labelTitre.ToString() + inputTitre.ToString() + labelCommentaire.ToString() + inputCommentaire.ToString() + stringButtonSubmit + stringHiddenIdCommentaireReference;

            using (self.BeginForm("Comment", "Hotel", new { id = idHotel }, FormMethod.Post, new { id = "FormulaireCommentaire" }))
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

            foreach (var com in commentaires.Where(c => c.IdCommentaireReference == idCommentaire).OrderByDescending(c => c.DateCommentaire))
            {
                CommentairesDiv(com, cpt, s);
            }

            divTag.InnerHtml = s.ToString();//
            return new MvcHtmlString(divTag.ToString());
        }

        public static void CommentairesDiv(Commentaires commentaire, int cpt, StringBuilder s)
        {
            s.Append(String.Format("<div class=\"CommentaireDiv col-xs-offset-{0} partial_view_search_result\">", cpt == 0 ? 0 : 1));
            s.Append(CommentaireDiv(commentaire));
            foreach (var com in commentaire.Commentaires1.OrderBy(c => c.DateCommentaire))
            {
                CommentairesDiv(com, cpt+1, s);
            }
            s.Append("</div>");
        }
        //public static void CommentairesDiv(List<Commentaires> commentaires, int? idCommentaire, int cpt, StringBuilder s)
        //{
        //    //if (cpt != 0)
        //    //{
        //    //    s.Append(String.Format("<div class=\"col-sm-offset-{0} partial_view_search_result\">", cpt));
        //    //}
        //    foreach (var com in commentaires.Where(c => c.IdCommentaireReference == idCommentaire).OrderBy(c=>c.DateCommentaire))
        //    {
        //       s.Append(CommentaireDiv(com, cpt));
        //       CommentairesDiv(commentaires, com.IdCommentaire, cpt+1, s);
        //    }
        //    //if (cpt != 0)
        //    //{
        //    //    s.Append("</div>");
        //    //}
        //}

        public static String CommentaireDiv(Commentaires commentaire) // int cpt
        {
            var db = new Form115Entities();

            // le <div> externe
            var divTag = new TagBuilder("div");
            //divTag.AddCssClass(String.Format("col-xs-offset-{0}", cpt));
            divTag.AddCssClass("Commentaire");


            // le <div> interne FirstLine (présentation)
            var divFirstLineTag = new TagBuilder("div");

            // le <span> interne titre
            var h5TitreTag = new TagBuilder("h5");
            h5TitreTag.InnerHtml = commentaire.Titre;
            h5TitreTag.AddCssClass("TitreCommentaire");
            h5TitreTag.AddCssClass("col-md-6 col-xs-4");

            // le <span> interne auteur
            var spanAuteurTag = new TagBuilder("b");
            spanAuteurTag.InnerHtml = String.Format("{0} {1}", commentaire.Utilisateurs.Prenom,  commentaire.Utilisateurs.Nom);
            spanAuteurTag.AddCssClass("AuteurCommentaire");
            spanAuteurTag.AddCssClass("text-center");
            spanAuteurTag.AddCssClass("col-md-3 col-xs-4");

            // le <span> interne date
            var spanDateTag = new TagBuilder("i");
            spanDateTag.InnerHtml = commentaire.DateCommentaire.ToShortDateString();
            spanDateTag.AddCssClass("DateCommentaire");
            spanDateTag.AddCssClass("text-right");
            spanDateTag.AddCssClass("col-md-3 col-xs-4");

            // Contenu du div interne FirstLine (présentation)
            divFirstLineTag.InnerHtml = h5TitreTag.ToString() + spanAuteurTag.ToString() + spanDateTag.ToString(); // 

            // le <p> interne texte
            var pTexteTag = new TagBuilder("p");
            pTexteTag.InnerHtml = commentaire.Commentaire;

            // le <div> pour le lien "Répondre"
            var divLastLineTag = new TagBuilder("div");

            // le <a> lien répondre
            var aRepondreTag = new TagBuilder("a");
            aRepondreTag.MergeAttribute("href", "#");
            aRepondreTag.AddCssClass("LienRepondre");
            aRepondreTag.MergeAttribute("id", String.Format("Commentaire-{0}",commentaire.IdCommentaire));
            aRepondreTag.InnerHtml = "Répondre";

            // Contenu du divpour le lien "Répondre"
            divLastLineTag.InnerHtml = aRepondreTag.ToString();
            divLastLineTag.AddCssClass("col-xs-offset-3");

            // Contenu du div externe
            divTag.InnerHtml = divFirstLineTag.ToString() + "<div class=\"clear\"></div>" + pTexteTag.ToString() + divLastLineTag.ToString();

            return divTag.ToString();
        }
    }
}