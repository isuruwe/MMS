namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Details
    {
        [Key]
        public int WDID { get; set; }

        
        [StringLength(500)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string Bed_No { get; set; }

        [StringLength(50)]
        public string Ward_No { get; set; }

        public string Remarks { get; set; }

        public int? Status { get; set; }

        [StringLength(500)]
        public string Created_By { get; set; }

        public DateTime? Created_Date { get; set; }

        [StringLength(500)]
        public string Modify_By { get; set; }

        public DateTime? Modify_Date { get; set; }

        [StringLength(50)]
        public string BHTNo { get; set; }

        [StringLength(50)]
        public string IsLiveOut { get; set; }

        public string Diagnosis { get; set; }

        [StringLength(20)]
        public string OPDID { get; set; }
    }
}
