namespace EPAS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EPASPharmacyItem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string itemno { get; set; }

        [StringLength(25)]
        public string itemshortdescription { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string itemdescription { get; set; }

        [StringLength(1)]
        public string itemclass { get; set; }

        [StringLength(10)]
        public string doq { get; set; }
    }
}
