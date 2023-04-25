namespace HRMS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class P3Context : DbContext
    {
        public P3Context()
            : base("name=P3Context")
        {
        }

        public virtual DbSet<rank> ranks { get; set; }
        public virtual DbSet<ServicePersonnelProfile> ServicePersonnelProfiles { get; set; }
        public virtual DbSet<View_1250> View_1250 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rank>()
                .Property(e => e.RNK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.LONG_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.TOTAL_SERVICE_FOR_PROMOTION_DE_LAC)
                .HasPrecision(18, 1);

            modelBuilder.Entity<rank>()
                .Property(e => e.TOTAL_SERVICE_FOR_PROMOTION_DE_ACPL)
                .HasPrecision(18, 1);

            modelBuilder.Entity<rank>()
                .Property(e => e.TEMPORY_SUBSTANTIVE)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.AppProm)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.AppPromType)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.NextAppProm)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.PromPaid)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.TOTAL_SERVICE_FOR_PROMOTION_DE)
                .HasPrecision(18, 1);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.SvcID)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.FingerID)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.TANo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ActiveNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.F1250)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.RR_No)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Intake)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Trade_Group)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.OtherNames)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Location)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Formation)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Current_Location)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Current_Formation)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Status)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.LivingIn_Out)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.NICNo_New)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Marriage_Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.NICNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.HashSno)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Appoinment)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.ActiveNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.F1250)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.NICNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Feet)
                .HasPrecision(4, 0);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Inch)
                .HasPrecision(4, 2);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.BirthMark)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.ServicingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Expr1)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.NICNo_New)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.DTE_ISU)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.BATCH_NO)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.SER_NO_CARD)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.NO_OF_CARDS)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.RNK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.TRD_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Posted_Location)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Posted_Formation)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.LivingIn_Out)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.DivisionName)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.OtherNames)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Marriage_Status)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
