namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SideEffect
    {
        [Key]
        [StringLength(15)]
        public string ItemID { get; set; }

        [Column("SideEffect")]
        [StringLength(50)]
        public string SideEffect1 { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
