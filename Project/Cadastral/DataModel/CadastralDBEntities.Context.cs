﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadastral.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CadastraDBEntities : DbContext
    {
        public CadastraDBEntities()
            : base("name=CadastraDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Immovable> Immovables { get; set; }
        public virtual DbSet<ImmovableType> ImmovableTypes { get; set; }
        public virtual DbSet<Land> Lands { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Cadastr> Cadastrs { get; set; }
        public virtual DbSet<LandType> LandTypes { get; set; }
        public virtual DbSet<LicenseRequest> LicenseRequests { get; set; }
    }
}
