﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPIWebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class XKPIEntities : DbContext
    {
        public XKPIEntities()
            : base("name=XKPIEntities")
        {
            
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CPAT_CHECK_RECORD> CPAT_CHECK_RECORD { get; set; }
        public virtual DbSet<CPAT_DIAGNOSIS> CPAT_DIAGNOSIS { get; set; }
        public virtual DbSet<CPAT_EMR_RECORD> CPAT_EMR_RECORD { get; set; }
        public virtual DbSet<CPAT_IN_ORDERS> CPAT_IN_ORDERS { get; set; }
        public virtual DbSet<CPAT_IN_PATIENT> CPAT_IN_PATIENT { get; set; }
        public virtual DbSet<CPAT_OUT_EMR> CPAT_OUT_EMR { get; set; }
        public virtual DbSet<CPAT_OUT_PATIENT> CPAT_OUT_PATIENT { get; set; }
        public virtual DbSet<CPAT_OUT_RECIPE> CPAT_OUT_RECIPE { get; set; }
        public virtual DbSet<CPAT_PATHOLOGY_RECORD> CPAT_PATHOLOGY_RECORD { get; set; }
        public virtual DbSet<CPAT_TEST_RECORD> CPAT_TEST_RECORD { get; set; }
        public virtual DbSet<CPAT_TEST_RESULT> CPAT_TEST_RESULT { get; set; }
        public virtual DbSet<CPAT_TEST_RESULT_GERM> CPAT_TEST_RESULT_GERM { get; set; }
    }
}
