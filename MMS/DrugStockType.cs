namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugStockType")]
    public partial class DrugStockType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockTypeID { get; set; }

        [StringLength(50)]
        public string StockType { get; set; }
    }
}
