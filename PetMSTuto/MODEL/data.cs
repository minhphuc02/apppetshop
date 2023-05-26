using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PetMSTuto.MODEL
{
    public partial class data : DbContext
    {
        public data()
            : base("name=data")
        {
        }

        public virtual DbSet<BillTbl> BillTbl { get; set; }
        public virtual DbSet<CustomerTbl> CustomerTbl { get; set; }
        public virtual DbSet<EmployeeTbl> EmployeeTbl { get; set; }
        public virtual DbSet<ProductTbl> ProductTbl { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
