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
    
    public partial class Statystyki
    {
        [Key]
        public int IdStat { get; set; }
        public int ilosc_glosow { get; set; }
        public int min_glos { get; set; }
        public int max_glos { get; set; }
        public decimal avg_glos { get; set; }
        public int Id_Dzielo { get; set; }
        public string Ankieta { get; set; }
    
        public virtual Dzieło Dzieło { get; set; }
    }
}
