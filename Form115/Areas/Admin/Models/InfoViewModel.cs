using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Form115.Areas.Admin.Models
{
    public class InfoViewModel
    {
        /// <summary>
        /// Titre html de la page.
        /// </summary>
        [Required]
        public string TitrePage { get; set; }

        /// <summary>
        /// Phrase nominale simple à afficher dans une police de grande taille en haut de la page.
        /// </summary>
        [Required]
        public string ResumeInfoTitre { get; set; }

        /// <summary>
        /// Texte décrivant l'information en détail. Il est affiché en caractères de taille normale.
        /// </summary>
        [Required]
        public string DetailInfo { get; set; }

        /// <summary>
        /// Element Html supplémentaire inséré en-dessous si besoin.
        /// </summary>
        public string Ajout { get; set; }

    }
}