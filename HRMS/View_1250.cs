namespace HRMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_1250
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ActiveNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string SNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Rank { get; set; }

        [StringLength(10)]
        public string F1250 { get; set; }

        [StringLength(50)]
        public string Initials { get; set; }

        public string Surname { get; set; }

        public int? Branch_Trade { get; set; }

        [StringLength(15)]
        public string NICNo { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        public decimal? Feet { get; set; }

        public decimal? Inch { get; set; }

        public int? ColourofHair { get; set; }

        public int? ColourofEyes { get; set; }

        public int? BloodGroup { get; set; }

        [StringLength(255)]
        public string BirthMark { get; set; }

        public DateTime? Expire_Date { get; set; }

        public DateTime? EXPIRES_DTE { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Service_Type { get; set; }

        public int? Service_Status { get; set; }

        [StringLength(40)]
        public string ServicingStatus { get; set; }

        public int? SVCType { get; set; }

        [StringLength(50)]
        public string Expr1 { get; set; }

        [StringLength(15)]
        public string NICNo_New { get; set; }

        [StringLength(100)]
        public string DTE_ISU { get; set; }

        [StringLength(10)]
        public string BATCH_NO { get; set; }

        [StringLength(25)]
        public string SER_NO_CARD { get; set; }

        [StringLength(100)]
        public string NO_OF_CARDS { get; set; }

        public DateTime? DateOfEnlist { get; set; }

        [StringLength(15)]
        public string RNK_NAME { get; set; }

        [StringLength(50)]
        public string TRD_NAME { get; set; }

        [StringLength(5)]
        public string Posted_Location { get; set; }

        public DateTime? ID_EXP_DAT { get; set; }

        [StringLength(50)]
        public string Posted_Formation { get; set; }

        [StringLength(3)]
        public string LivingIn_Out { get; set; }

        [StringLength(60)]
        public string DivisionName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public string OtherNames { get; set; }

        [StringLength(3)]
        public string Marriage_Status { get; set; }
    }
}
