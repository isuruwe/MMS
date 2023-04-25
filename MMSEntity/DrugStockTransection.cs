namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugStockTransection")]
    public partial class DrugStockTransection
    {
        [Key]
        public long TransectionID { get; set; }

        [StringLength(50)]
        public string StockID { get; set; }

        [Required]
        [StringLength(15)]
        public string ItemID { get; set; }

        [Required]
        [StringLength(50)]
        public string IssuedTo { get; set; }

        public DateTime IssuedDate { get; set; }

        public long BatchID { get; set; }

        public decimal TransectionQty { get; set; }

        public int IssuedUser { get; set; }
    }
}
