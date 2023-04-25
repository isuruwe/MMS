namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpouseDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string SNo { get; set; }

        [StringLength(500)]
        public string SpouseName { get; set; }

        [StringLength(10)]
        public string NICNo { get; set; }
    }
}
