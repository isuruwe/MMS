namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryFrequency")]
    public partial class SurgeryFrequency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pomfid { get; set; }

        [StringLength(500)]
        public string pomfdesc { get; set; }
    }
}
