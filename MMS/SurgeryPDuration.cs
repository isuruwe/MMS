namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryPDuration")]
    public partial class SurgeryPDuration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pomdid { get; set; }

        [StringLength(500)]
        public string pomduration { get; set; }
    }
}
