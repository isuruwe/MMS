namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryAssist")]
    public partial class SurgeryAssist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int asid { get; set; }

        [StringLength(500)]
        public string asidesc { get; set; }
    }
}
