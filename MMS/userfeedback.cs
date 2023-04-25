namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userfeedback")]
    public partial class userfeedback
    {
        [Key]
        public long fid { get; set; }

        public int? userid { get; set; }

        public string comment { get; set; }

        public DateTime? createdate { get; set; }
    }
}
