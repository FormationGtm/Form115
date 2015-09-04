using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Models
{
    public class CommentViewModel
    {
        public string Titre { get; set; }
        public string Commentaire { get; set; }

        // Commentaire Référencé
        public int? IdCommentaire { get; set; }

        // Paramètre passé à la vue
        public Hotels Hotel { get; set; }
    }
}