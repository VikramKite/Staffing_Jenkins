﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace staffing.data.ef
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InternalSystemEntities : DbContext
    {
        public InternalSystemEntities()
            : base("name=InternalSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<account_manager> account_manager { get; set; }
        public virtual DbSet<assigned_to_whom> assigned_to_whom { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<job_location> job_location { get; set; }
        public virtual DbSet<job_title> job_title { get; set; }
        public virtual DbSet<position_status> position_status { get; set; }
        public virtual DbSet<position_type> position_type { get; set; }
        public virtual DbSet<logging> loggings { get; set; }
        public virtual DbSet<client_manager> client_manager { get; set; }
        public virtual DbSet<staffing> staffings { get; set; }
    
        public virtual ObjectResult<SP_GetPositionStatus_Result> SP_GetPositionStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetPositionStatus_Result>("SP_GetPositionStatus");
        }
    
        public virtual int SP_GetClientWiseReport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetClientWiseReport");
        }
    }
}
