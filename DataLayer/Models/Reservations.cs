//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservations
    {
        public int IdReservation { get; set; }
        public int IdProduit { get; set; }
        public int Quantity { get; set; }
        public int IdUtilisateur { get; set; }
    
        public virtual Produits Produits { get; set; }
    }
}
