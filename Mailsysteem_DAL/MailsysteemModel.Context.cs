﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mailsysteem_DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MailsysteemEntities : DbContext
    {
        public MailsysteemEntities()
            : base("name=MailsysteemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bericht> Bericht { get; set; }
        public virtual DbSet<BerichtOntvanger> BerichtOntvanger { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Gebruiker> Gebruiker { get; set; }
        public virtual DbSet<Taak> Taak { get; set; }
        public virtual DbSet<TaakCategorieRepo> TaakCategorie { get; set; }
        public virtual DbSet<Vergadering> Vergadering { get; set; }
        public virtual DbSet<VergaderingGenodigde> VergaderingGenodigde { get; set; }
        public virtual DbSet<Woonplaats> Woonplaats { get; set; }
    }
}
