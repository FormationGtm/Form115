using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Form115.Areas.Admin.Models
{
    public class LettreInfoViewModel
    {
    
        [Required]
        public string Objet {get; set; }

        [Required]
        public string Corps {get; set; }

        public string AdresseLien {get; set; }

    }
}