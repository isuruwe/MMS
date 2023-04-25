namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hypersensivity")]
    public partial class Hypersensivity
    {
        [Key]
        [StringLength(50)]
        public string HypersenseID { get; set; }

        
        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string HyperTypeSubID { get; set; }

        public int? RSubID { get; set; }

        public int? SeverityID { get; set; }

        [StringLength(500)]
        public string HypersenseDetail { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual HypMainCategory HypMainCategory { get; set; }
    }
}
