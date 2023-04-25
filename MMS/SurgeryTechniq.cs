namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryTechniq")]
    public partial class SurgeryTechniq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tid { get; set; }

        [StringLength(500)]
        public string tdesc { get; set; }
    }
}
