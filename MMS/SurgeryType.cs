namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryType")]
    public partial class SurgeryType
    {
        [Key]
        public int TAID { get; set; }

        [StringLength(50)]
        public string TADescription { get; set; }
    }
}
