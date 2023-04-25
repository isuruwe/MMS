namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("medreport")]
    public partial class medreport
    {
        [Key]
        public long did { get; set; }

        [StringLength(500)]
        public string dname { get; set; }

        [StringLength(50)]
        public string pid { get; set; }

        public DateTime? createddate { get; set; }

        public int? createdby { get; set; }

        public byte[] docdata { get; set; }
    }
}
