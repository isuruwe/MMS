namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryPom")]
    public partial class SurgeryPom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pomid { get; set; }

        [StringLength(1000)]
        public string pomdesc { get; set; }
    }
}
