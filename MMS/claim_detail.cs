namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class claim_detail
    {
        [Key]
        [StringLength(100)]
        public string claim_id { get; set; }

        [StringLength(50)]
        public string pid { get; set; }

        public DateTime? claim_date { get; set; }

        [StringLength(50)]
        public string mcat_id { get; set; }

        [StringLength(500)]
        public string dgid { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(3)]
        public string loc { get; set; }

        [StringLength(100)]
        public string ClaimName { get; set; }

        public decimal? ClaimAmount { get; set; }

        [StringLength(50)]
        public string MobileNo { get; set; }

        public int? Status { get; set; }

        public decimal? Doc_Amount { get; set; }

        [StringLength(50)]
        public string RegisterNo { get; set; }

        public decimal? DHS_Amount { get; set; }

        public decimal? CWF_Amount { get; set; }

        public decimal? TSI_Amount { get; set; }

        public string ClaimReturn { get; set; }

        public int? IsMinit { get; set; }

        [StringLength(500)]
        public string Authority { get; set; }

        [StringLength(50)]
        public string Nurse { get; set; }

        [StringLength(50)]
        public string DHS { get; set; }

        [StringLength(50)]
        public string TSI { get; set; }

        public DateTime? Nurse_Date { get; set; }

        public DateTime? DHS_Date { get; set; }

        public DateTime? TSI_Date { get; set; }

        public int? IsDHSConfirmed { get; set; }
    }
}
