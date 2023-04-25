namespace MMS
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

      
        [StringLength(50)]
        public string PDID { get; set; }

        
        [StringLength(10)]
        public string LabTestID { get; set; }

        [StringLength(500)]
        public string teststatus { get; set; }

        [StringLength(500)]
        public string testResult { get; set; }

       
        [StringLength(10)]
        public string RequestedLocID { get; set; }

        [StringLength(10)]
        public string Issued { get; set; }

        public DateTime? IssuedTime { get; set; }

        public DateTime? RequestedTime { get; set; }

        [StringLength(10)]
        public string IssuedLocID { get; set; }

        public byte[] LabImg { get; set; }

        [StringLength(500)]
        public string TestSID { get; set; }

        [StringLength(10)]
        public string IsPrint { get; set; }

        public int? Isemail { get; set; }

        public int? IsApproved { get; set; }
        public int? ApprovedUser { get; set; }
    }
}
