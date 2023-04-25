namespace EPAS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EPASContext : DbContext
    {
        public EPASContext()
            : base("name=EPASContext")
        {
        }

        public virtual DbSet<EPASPharmacyItem> EPASPharmacyItems { get; set; }
        public virtual DbSet<MedicalItemDetailSnMD> MedicalItemDetailSnMDs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EPASPharmacyItem>()
                .Property(e => e.itemclass)
                .IsFixedLength();

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.RequisitionAllocationNo)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.DOQ)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.ItemClass)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.IssueDate)
                .IsUnicode(false);
        }
    }
}
