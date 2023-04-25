namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class parent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string SNo { get; set; }

        [StringLength(6)]
        public string Relationship { get; set; }

        [StringLength(150)]
        public string ParentName { get; set; }
    }
}
