namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STATIONS")]
    public partial class STATION
    {
        [Key]
        [StringLength(255)]
        public string STN_CODE { get; set; }

        [StringLength(255)]
        public string STN_NAME { get; set; }

        [StringLength(255)]
        public string BASE { get; set; }

        [StringLength(255)]
        public string AUD_REF { get; set; }

        [StringLength(255)]
        public string SHORT_NAME { get; set; }

        [StringLength(255)]
        public string ABRI { get; set; }
    }
}
