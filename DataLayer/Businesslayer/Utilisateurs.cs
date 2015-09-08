using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public partial class Utilisateurs
    {

        Form115Entities db = new Form115Entities();

        public bool EstClient
        {
            get
            {
                return Reservations.Any();
            }
        }

        public bool EstAdmin
        {
            get
            {
                return AspNetUsers.AspNetRoles.Where(r=>r.Name=="Admin").Any() ;
            }
        }

        public int getReducBase()
        {
            if (Reservations
                    .Where(resa => resa.Produits.DateDepart >= DateTime.Now.AddMonths(-6))
                    .Sum(resa => resa.Produits.Prix)
                    > 150)
            {
                return 20;
            }
            else if (Reservations
                  .Where(resa => resa.Produits.DateDepart >= DateTime.Now.AddYears(-1))
                  .Sum(resa => resa.Produits.Prix)
                  > 250)
            {
                return 15;
            }
            else if (Reservations
                  .Where(resa => resa.Produits.DateDepart >= DateTime.Now.AddYears(-2))
                  .Sum(resa => resa.Produits.Prix)
                  > 400)
            {
                return 10;
            }
            else if (EstClient)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        public int getReducTemp()
        {
            if (Reservations
                .Where(resa => resa.Produits.DateDepart >= DateTime.Now.AddMonths(-4))
                .Any())
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

    }
}
