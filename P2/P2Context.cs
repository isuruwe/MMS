namespace P2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class P2Context : DbContext
    {
        public P2Context()
            : base("name=P2Context")
        {
        }

        public virtual DbSet<F373_Image> F373_Image { get; set; }
        public virtual DbSet<rank> ranks { get; set; }
        public virtual DbSet<ServicePersonnelProfile> ServicePersonnelProfiles { get; set; }
        public virtual DbSet<View_1250> View_1250 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<F373_Image>()
                .Property(e => e.ImageID)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.SvcID)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.UniformType)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.CreatedUser)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.CreatedMachine)
                .IsUnicode(false);

            modelBuilder.Entity<F373_Image>()
                .Property(e => e.ModifiedUser)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.RANK1)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.RNK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.AUD_REF)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.LONG_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.FILE_REF)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.SUBSTANTIVETEMP)
                .IsFixedLength();

            modelBuilder.Entity<rank>()
                .Property(e => e.PROMNAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.SVCCONFM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.Vacancy)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.VacancyCode)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.RnkNameSln)
                .IsUnicode(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.RnkNameTamil)
                .IsUnicode(false);

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
                .Property(e => e.CadetNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.TANo)
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
                .Property(e => e.Appointment)
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
                .Property(e => e.NICNo)
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
                .Property(e => e.ActiveNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Eligibility)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.SNoNew)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.F1250)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.BKServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.HashSno)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.ActiveNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.CadetNo)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.BirthMark)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.NIC_NO)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.DateOfBirth)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.ExpireDate)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.OtherName)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.F1250_No)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.DateOfEnlist)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.TRD_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.RNK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<View_1250>()
                .Property(e => e.SNo)
                .IsUnicode(false);
        }
    }
}
