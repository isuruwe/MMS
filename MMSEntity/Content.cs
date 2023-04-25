namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string ItemID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ItemContent { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Qty { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
