namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PastMedHistory")]
    public partial class PastMedHistory
    {
        [Key]
        public long PMHID { get; set; }

       
        [StringLength(50)]
        public string PID { get; set; }

        public string PMHDetail { get; set; }

        public string Drughst { get; set; }
    }
}
