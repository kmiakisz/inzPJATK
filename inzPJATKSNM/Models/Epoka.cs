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
    
    public partial class Epoka
    {
        public Epoka()
        {
            this.Autor = new HashSet<Autor>();
        }
    
        public int Id_Epoki { get; set; }
        public string Epoka1 { get; set; }
    
        public virtual ICollection<Autor> Autor { get; set; }
    }
}
