namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Surgeom")]
    public partial class Surgeom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int moid { get; set; }

        [StringLength(500)]
        public string modesc { get; set; }
    }
}
