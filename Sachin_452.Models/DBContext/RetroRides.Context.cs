﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sachin_452.Models.DBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Sachin_452Entities : DbContext
    {
        public Sachin_452Entities()
            : base("name=Sachin_452Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserRideTable> UserRideTable { get; set; }
        public virtual DbSet<ZipCode> ZipCode { get; set; }
        public virtual DbSet<BikeDetails> BikeDetails { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<Favourites> Favourites { get; set; }
    }
}
