namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MBPS_View
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string MED16_ID { get; set; }

        [StringLength(10)]
        public string Ser_No { get; set; }

        [StringLength(20)]
        public string Rank { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(200)]
        public string OtherNames { get; set; }

        [StringLength(20)]
        public string Loacation { get; set; }

        [StringLength(20)]
        public string Branch_Trade { get; set; }

        public int? ServiceYrs { get; set; }

        public int? ServiceMnths { get; set; }

        public DateTime? CeasedDutyDate { get; set; }

        [StringLength(250)]
        public string AuthOfBoard { get; set; }

        public DateTime? StartDate { get; set; }

        [StringLength(250)]
        public string PlaceOfOrigin { get; set; }

        [StringLength(500)]
        public string PrincipleDisability { get; set; }

        [StringLength(500)]
        public string OtherDisability { get; set; }

        public DateTime? DateOfBoard { get; set; }

        [StringLength(250)]
        public string PlaceOfBoard { get; set; }

        public string PresentCondition { get; set; }

        public int? Clerk_Auth { get; set; }

        public int? Mem1_Auth { get; set; }

        public int? Mem2_Auth { get; set; }

        public int? Pres_Auth { get; set; }

        public int? Mem3_Auth { get; set; }

        public int? Pulheems_P { get; set; }

        public int? Pulheems_U { get; set; }

        public int? Pulheems_L { get; set; }

        public int? Pulheems_H { get; set; }

        public int? Pulheems_E_R { get; set; }

        public int? Pulheems_E_L { get; set; }

        public int? Pulheems_M { get; set; }

        public int? Pulheems_S { get; set; }

        [StringLength(250)]
        public string PES { get; set; }

        [StringLength(10)]
        public string UOM_Hight { get; set; }

        [StringLength(5)]
        public string CP1 { get; set; }

        [StringLength(10)]
        public string UOM_Wight { get; set; }

        [StringLength(10)]
        public string Hight { get; set; }

        [StringLength(10)]
        public string Wight { get; set; }

        public int? YOB { get; set; }

        [StringLength(250)]
        public string BoardPES { get; set; }

        public string PESNEW { get; set; }

        public string Restrict_n_Emp { get; set; }

        [StringLength(10)]
        public string MedCat_13b { get; set; }

        public DateTime? UnStartDay { get; set; }

        public DateTime? UnEndDay { get; set; }

        public string OtherDesc { get; set; }

        public string Instru_Individual { get; set; }

        public DateTime? SickLeaveFrom { get; set; }

        public DateTime? SickLeaveTo { get; set; }

        public DateTime? DateOfNextMedical { get; set; }

        [StringLength(10)]
        public string President { get; set; }

        [StringLength(10)]
        public string Member1 { get; set; }

        [StringLength(10)]
        public string Member2 { get; set; }

        [StringLength(10)]
        public string Member3 { get; set; }

        [StringLength(5)]
        public string ConfirmingOfficer { get; set; }

        public DateTime? ConfirmingDate { get; set; }

        [StringLength(5)]
        public string ConRank { get; set; }

        [StringLength(5)]
        public string ConApp { get; set; }

        public int? Pulheems_Pp { get; set; }

        [StringLength(50)]
        public string CP2 { get; set; }

        public int? Pulheems_Uu { get; set; }

        public int? Pulheems_Ll { get; set; }

        public int? Pulheems_Ss { get; set; }

        public int? FTB_P1 { get; set; }

        public int? FTB_U1 { get; set; }

        public int? FTB_L1 { get; set; }

        public int? FTB_H1 { get; set; }

        public int? FTB_E_R1 { get; set; }

        public int? FTB_E_L1 { get; set; }

        public int? FTB_M1 { get; set; }

        public int? FTB_S1 { get; set; }

        public int? FTB_Pp1 { get; set; }

        public int? FTB_Uu1 { get; set; }

        public int? FTB_Ll1 { get; set; }

        public int? FTB_Ss1 { get; set; }

        [StringLength(500)]
        public string Member1Remark { get; set; }

        [StringLength(500)]
        public string Member2Remark { get; set; }

        [StringLength(500)]
        public string Member3Remark { get; set; }

        [StringLength(500)]
        public string PresidentRemark { get; set; }

        [StringLength(5)]
        public string Is_Mem1_Auth { get; set; }

        [StringLength(5)]
        public string Is_Mem2_Auth { get; set; }

        [StringLength(5)]
        public string Is_Mem3_Auth { get; set; }

        [StringLength(5)]
        public string Is_Presi_Auth { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Active { get; set; }

        [StringLength(1500)]
        public string Remarks { get; set; }

        public DateTime? ExpireDate { get; set; }

        [StringLength(5)]
        public string EnterDate { get; set; }

        [StringLength(5)]
        public string F551 { get; set; }

        [StringLength(5)]
        public string Reason { get; set; }

        public DateTime? PresFwdDate { get; set; }

        [StringLength(50)]
        public string SNo { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string RankName { get; set; }

        [Column("Branch/Trade")]
        [StringLength(50)]
        public string Branch_Trade1 { get; set; }
    }
}
