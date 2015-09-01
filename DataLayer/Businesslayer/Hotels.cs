using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public partial class Hotels
    {

        private readonly Form115Entities _db = new Form115Entities();

        public int NbReservations
        {
            get
            {
                return _db.Reservations.Where(r => r.Produits.Sejours.Hotels.IdHotel == IdHotel).Select(r => r.Quantity).Sum();
            }
        }

        //// TODO nompbre de reservations sur les x derniers jours => sauvegardés les dates des réservations en BDD
        //public int NbReservations(int periode)
        //{
        //    return _db.Reservations.Where(r => r.Produits.Sejours.IdHotel == 1002 TimeSpan
        //                                        && (DateTime.Now - r.DateReservation).TotalDays >= periode)
        //                           .Select(r => r.Quantity).Sum();
        //}


        public void ajouterPromotion(Form115Entities db, Promotions promo)
        {
            bool commencePendantExistant = Promotions.Where(p => p.DateDebut <= promo.DateDebut && p.DateFin >= promo.DateDebut).Any();
            bool terminePendantExistant = Promotions.Where(p => p.DateDebut <= promo.DateFin && p.DateFin >= promo.DateFin).Any();
            if (commencePendantExistant || terminePendantExistant)
            {
                throw new Exception();
            }
            else
            {
                db.Promotions.Add(promo);
            }
        }
    }
}
