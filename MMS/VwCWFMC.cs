namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VwCWFMCS")]
    public partial class VwCWFMC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clsSerialNo { get; set; }

        [StringLength(50)]
        public string claRegNo { get; set; }

        [StringLength(50)]
        public string claSno { get; set; }

        [StringLength(50)]
        public string claCatCode { get; set; }

        public decimal? claBillAmount { get; set; }

        [StringLength(50)]
        public string claAuthorityForCDD { get; set; }

        [StringLength(50)]
        public string claAuthorityForOverBillAmount { get; set; }

        public int? claClaimYear { get; set; }

        public int? claMonth { get; set; }

        public int? claChildBirthCount { get; set; }

        public int? clsStatus { get; set; }

        public DateTime? clsDate { get; set; }

        public DateTime? claSaveDate { get; set; }

        public int? IsProcessed { get; set; }

        public int? IsRetired { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        public decimal? appliedBillAmount { get; set; }

        [StringLength(50)]
        public string RegistrationId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int? RankId { get; set; }

        public string Diagnosis { get; set; }

        public decimal? ApprovedTSI { get; set; }

        [StringLength(250)]
        public string Beneficiary { get; set; }

        public DateTime? AppliedDate { get; set; }
    }
}
