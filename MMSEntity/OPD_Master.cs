namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OPD_Master
    {
        [Key]
        [StringLength(50)]
        public string OPD_ID { get; set; }

        [StringLength(50)]
        public string CurrentNo { get; set; }

        [StringLength(50)]
        public string MaxPatients { get; set; }

        [StringLength(500)]
        public string OPD_Detail { get; set; }

        [StringLength(3)]
        public string LocationID { get; set; }

        public virtual Location Location { get; set; }
    }
}
