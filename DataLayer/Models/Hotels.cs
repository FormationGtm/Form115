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
    
    public partial class Hotels
    {
        public Hotels()
        {
            this.Sejours = new HashSet<Sejours>();
        }
    
        public int IdHotel { get; set; }
        public Nullable<int> IdVille { get; set; }
        public Nullable<byte> Categorie { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Nom { get; set; }
    
        public virtual ICollection<Sejours> Sejours { get; set; }
        public virtual Villes Villes { get; set; }
    }
}
