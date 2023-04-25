namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Child
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string SNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceType { get; set; }

        [StringLength(500)]
        public string ChildName { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public long? Child_No { get; set; }
    }
}
