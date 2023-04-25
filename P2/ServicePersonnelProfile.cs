namespace P2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServicePersonnelProfile")]
    public partial class ServicePersonnelProfile
    {
        [Key]
        [StringLength(15)]
        public string SNo { get; set; }

        [StringLength(20)]
        public string SvcID { get; set; }

        [StringLength(10)]
        public string FingerID { get; set; }

        [StringLength(10)]
        public string ServiceNo { get; set; }

        [StringLength(50)]
        public string CadetNo { get; set; }

        [StringLength(50)]
        public string TANo { get; set; }

        [StringLength(50)]
        public string RR_No { get; set; }

        public int? Rank { get; set; }

        public int? IntakeType { get; set; }

        [StringLength(10)]
        public string Intake { get; set; }

        public int? Branch_Trade { get; set; }

        public DateTime? CommissionedDate { get; set; }

        [StringLength(50)]
        public string Trade_Group { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string Initials { get; set; }

        [StringLength(30)]
        public string Appointment { get; set; }

        public int? Service_Type { get; set; }

        public int? Service_Status { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Date_of_Enlist { get; set; }

        public DateTime? Expire_Date { get; set; }

        public DateTime? Date_of_PresentRank { get; set; }

        [StringLength(5)]
        public string Posted_Location { get; set; }

        [StringLength(50)]
        public string Posted_Formation { get; set; }

        [StringLength(5)]
        public string Current_Location { get; set; }

        [StringLength(5)]
        public string Current_Formation { get; set; }

        [StringLength(5)]
        public string Posted_Status { get; set; }

        [StringLength(3)]
        public string LivingIn_Out { get; set; }

        public int? Status { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [StringLength(12)]
        public string NICNo { get; set; }

        [StringLength(3)]
        public string Marriage_Status { get; set; }

        public byte[] Picture { get; set; }

        public decimal? PromScheduleSeniority { get; set; }

        public int? Seniority { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(15)]
        public string CreatedMachine { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(15)]
        public string ModifiedMachine { get; set; }

        [StringLength(10)]
        public string ActiveNo { get; set; }

        public string Remark { get; set; }

        public DateTime? DueDate { get; set; }

        [StringLength(5)]
        public string Eligibility { get; set; }

        public DateTime? DueDate_4mat { get; set; }

        public string FullNameSln { get; set; }

        [StringLength(15)]
        public string SNoNew { get; set; }

        [StringLength(50)]
        public string F1250 { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProfilePicture { get; set; }

        public int? MedCat { get; set; }

        [StringLength(10)]
        public string BKServiceNo { get; set; }

        public string HashSno { get; set; }
    }
}
