﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelloWorld.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EnterpriseApplication> EnterpriseApplications { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<ServiceLog> ServiceLogs { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }
        public virtual DbSet<ServiceVersion> ServiceVersions { get; set; }
        public virtual DbSet<Similar> Similars { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
    }
}
