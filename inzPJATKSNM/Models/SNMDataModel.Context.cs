﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class inzSNMEntities : DbContext
    {
        public inzSNMEntities()
            : base("name=inzSNMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Ankieta> Ankieta { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Bierze_udzial> Bierze_udzial { get; set; }
        public virtual DbSet<Dzieło> Dzieło { get; set; }
        public virtual DbSet<Epoka> Epoka { get; set; }
        public virtual DbSet<Glosujący> Glosujący { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Muzyka> Muzyka { get; set; }
        public virtual DbSet<Narodowosc> Narodowosc { get; set; }
        public virtual DbSet<Ocena> Ocena { get; set; }
        public virtual DbSet<Plec> Plec { get; set; }
        public virtual DbSet<Statystyki> Statystyki { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Technika> Technika { get; set; }
        public virtual DbSet<Wiek> Wiek { get; set; }
    }
}
