using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Form115.Models
{
    public class HotelViewModel
    {

        public int IdHotel { get; set; }

        public int DureeMinSejour { get; set; }

        public int? DureeMaxSejour { get; set; }

        public DateTime _dateDepart;
        public string DateDepart
        {
            get { return _dateDepart.ToString(); }
            set
            {
                if (value == "") { _dateDepart = DateTime.Now; }
                else {
                    string format = "dd/MM/yyyy";
                    if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out _dateDepart))
                    {
                        _dateDepart = DateTime.Now;
                    }
                }
            }
        }


        // Gestion de l'affichage des séjours d'un hôtel sur une période
        public DateTime _dateDebut;
        public string DateDebut
        {
            get { return _dateDebut.ToString(); }
            set
            {
                if (value == "") { _dateDebut = DateTime.Now; }
                else
                {
                    string format = "yyyy-MM-dd";
                    if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out _dateDebut))
                    {
                        _dateDebut = DateTime.Now;
                    }
                }
            }
        }
        public DateTime _dateFin;
        public string DateFin
        {
            get { return _dateFin.ToString(); }
            set
            {
                if (value == "") { _dateFin = DateTime.MaxValue; }
                else
                {
                    string format = "yyyy-MM-dd";
                    if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out _dateFin))
                    {
                        _dateFin = DateTime.MaxValue;
                    }
                }
            }
        }

        public int NbPers { get; set; }

    }
}