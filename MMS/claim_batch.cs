namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class claim_batch
    {
        [Key]
        public long cmid { get; set; }

        public string batchid { get; set; }

        public int? userid { get; set; }

        public string claim_id { get; set; }

        public DateTime? BatchDate { get; set; }

        public int? UserCaT { get; set; }
    }
}
