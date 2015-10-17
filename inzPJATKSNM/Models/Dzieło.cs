//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace inzPJATKSNM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dzieło
    {
        public Dzieło()
        {
            this.Ocena = new HashSet<Ocena>();
            this.Statystyki = new HashSet<Statystyki>();
            this.Ankieta = new HashSet<Ankieta>();
        }
    
        public int Id_dzieło { get; set; }
        public string URL { get; set; }
        public int Id_Tech { get; set; }
        public int Id_Kat { get; set; }
        public string Rozmiar { get; set; }
        public int Id_Autora { get; set; }
    
        public virtual Autor Autor { get; set; }
        public virtual ICollection<Ocena> Ocena { get; set; }
        public virtual ICollection<Statystyki> Statystyki { get; set; }
        public virtual Kategoria Kategoria { get; set; }
        public virtual Technika Technika { get; set; }
        public virtual ICollection<Ankieta> Ankieta { get; set; }
    }
}
