namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HypersensivityType")]
    public partial class HypersensivityType
    {
        [Key]
        [StringLength(50)]
        public string HyperTypeID { get; set; }

        [StringLength(50)]
        public string HyperType { get; set; }

        [StringLength(50)]
        public string HyperSubType { get; set; }

        public virtual HypMainCategory HypMainCategory { get; set; }
    }
}
