namespace MMSEntity
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

        [Required]
        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string HyperTypeSubID { get; set; }

        public int RSubID { get; set; }

        public int SeverityID { get; set; }

        [StringLength(500)]
        public string HypersenseDetail { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual HypersenseReaction HypersenseReaction { get; set; }

        public virtual HypersenseSeverty HypersenseSeverty { get; set; }

        public virtual HypersensivityType HypersensivityType { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
