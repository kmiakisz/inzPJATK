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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Bierze_udzial
    {
        public Bierze_udzial()
        {
            this.Ocena = new HashSet<Ocena>();
        }
        [ForeignKey("Glosujący")]
        public int Id_Osoba { get; set; }
        [Key]
        public int Id_ankiety { get; set; }
    
        public virtual Ankieta Ankieta { get; set; }
        public virtual ICollection<Ocena> Ocena { get; set; }
        public virtual Glosujący Glosujący { get; set; }
    }
}
