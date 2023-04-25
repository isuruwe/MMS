namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SickReport")]
    public partial class SickReport
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string svcid { get; set; }

        [StringLength(50)]
        public string ismarried { get; set; }

        [StringLength(50)]
        public string isliveout { get; set; }

        public int? age { get; set; }

        public double? service { get; set; }

        [StringLength(50)]
        public string isduty { get; set; }

        [StringLength(50)]
        public string islow { get; set; }

        public DateTime? regdate { get; set; }

        [StringLength(50)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string OPDID { get; set; }

        public int? SRType { get; set; }

        public int? createdby { get; set; }

        public int? modifiedby { get; set; }

        public DateTime? modifieddate { get; set; }

        public int? IsMedical { get; set; }

        [StringLength(50)]
        public string ISDental { get; set; }
    }
}
