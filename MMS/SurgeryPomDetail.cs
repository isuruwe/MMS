namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryPomDetail")]
    public partial class SurgeryPomDetail
    {
        [Key]
        public long pomid { get; set; }

        [StringLength(50)]
        public string pdid { get; set; }

        [StringLength(50)]
        public string pcatid { get; set; }

        public int? pfeqid { get; set; }

        public int? pduid { get; set; }

        public string pdetail { get; set; }
    }
}
