﻿using konkeror.data.Domain;
using System;
using System.Data.Entity;
using System.Diagnostics;

namespace konkeror.data
{
    public class konkerorEntities : DbContext
    {
        public konkerorEntities()
            :base("name=konkerordb")
        {
        }


        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Devise> Devises { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.Log = (query) => Debug.Write(query);
            //modelBuilder.Entity<License>().HasRequired(e => e.Client).WithMany().HasForeignKey(e => e.ClientId);
        }
    }
}
