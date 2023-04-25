namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lab_Report
    {
        [Key]
        [StringLength(50)]
        public string Lab_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string PDID { get; set; }

        [Required]
        [StringLength(10)]
        public string LabTestID { get; set; }

        [StringLength(500)]
        public string Result { get; set; }

        [Required]
        [StringLength(10)]
        public string RequestedLocID { get; set; }

        [StringLength(10)]
        public string Issued { get; set; }

        public DateTime? IssuedTime { get; set; }

        public DateTime? RequestedTime { get; set; }

        [StringLength(10)]
        public string IssuedLocID { get; set; }

        public byte[] LabImg { get; set; }

        public virtual Lab_SubCategory Lab_SubCategory { get; set; }

        public virtual Patient_Detail Patient_Detail { get; set; }
    }
}
