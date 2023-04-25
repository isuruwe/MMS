namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TranferDetail
    {
        [Key]
        [StringLength(50)]
        public string TransID { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(10)]
        public string FromLoc { get; set; }

        [StringLength(10)]
        public string ToLoc { get; set; }

        public DateTime? TransferDate { get; set; }

        public int? TransStatus { get; set; }
    }
}
