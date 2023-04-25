namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisualExamination")]
    public partial class VisualExamination
    {
        [Key]
        [StringLength(10)]
        public string VSID { get; set; }

        [StringLength(10)]
        public string PDID { get; set; }

        public byte[] Image { get; set; }
    }
}
