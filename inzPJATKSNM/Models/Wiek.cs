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
    
    public partial class Wiek
    {
        public Wiek()
        {
            this.Glosujący = new HashSet<Glosujący>();
        }
    
        public int Id_Wiek { get; set; }
        public string Wiek1 { get; set; }
    
        public virtual ICollection<Glosujący> Glosujący { get; set; }
    }
}
