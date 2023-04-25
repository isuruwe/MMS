namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usage")]
    public partial class Usage
    {
        [Key]
        [StringLength(15)]
        public string ItemID { get; set; }

        [Column("Usage")]
       
        [StringLength(20)]
        public string Usage1 { get; set; }

        [StringLength(20)]
        public string Remarks { get; set; }
    }
}
