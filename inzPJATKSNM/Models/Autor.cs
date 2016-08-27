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
    
    public partial class Autor
    {
        public Autor()
        {
            this.Dzieło = new HashSet<Dzieło>();
        }
        [Key]
        public int Id_Autora { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        public int Id_Narod { get; set; }
        public int Id_Plec { get; set; }
        public int Id_Epoki { get; set; }
    
        public virtual ICollection<Dzieło> Dzieło { get; set; }
        public virtual Epoka Epoka { get; set; }
        public virtual Narodowosc Narodowosc { get; set; }
        public virtual Plec Plec { get; set; }
    }
}
