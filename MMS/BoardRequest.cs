namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoardRequest")]
    public partial class BoardRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReqID { get; set; }

        [StringLength(50)]
        public string ServiceNo { get; set; }

        [StringLength(50)]
        public string Current_Stn { get; set; }

        [StringLength(50)]
        public string Present_Formation { get; set; }

        [StringLength(50)]
        public string TypeOfBoard { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextMBdate { get; set; }

        public string ReasonForEarly { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dof_Onset { get; set; }

        public string HistoryOfDisease { get; set; }

        public string Present_Condition { get; set; }

        public string Clinic_Details { get; set; }

        public string Oppinion_Of_Consult { get; set; }

        [StringLength(50)]
        public string F551 { get; set; }

        [StringLength(50)]
        public string RelatedTo_Svc { get; set; }

        public string BoardReqRemarks { get; set; }

        [StringLength(20)]
        public string withoutSpec_L { get; set; }

        [StringLength(20)]
        public string withoutSpec_R { get; set; }

        public int? ReqStatus { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? RecommendBY { get; set; }

        public DateTime? RecommendDate { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? ConfirmBy { get; set; }

        public DateTime? ConfirmDate { get; set; }

        [StringLength(50)]
        public string RejectBy { get; set; }

        [StringLength(200)]
        public string RejectReason { get; set; }
    }
}
