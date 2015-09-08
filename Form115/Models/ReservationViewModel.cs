using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Models
{
    public class ReservationViewModel
    {
        public int IdProduit { get; set; }
        public int Quantity { get; set; }
        public string IdUtilisateur { get; set; }
        public DateTime DateReservation { get; set; }
    }
}