using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Models
{
    public class SearchResutPartialViewItem
    {
        public Hotels Hotel { get; set; }
        public List<Produits> Produits { get; set; }
    }
}