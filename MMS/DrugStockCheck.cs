namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugStockCheck")]
    public partial class DrugStockCheck
    {
        [Key]
        [StringLength(50)]
        public string grugindex { get; set; }

        [StringLength(50)]
        public string drugid { get; set; }

        [StringLength(20)]
        public string storeid { get; set; }

        [StringLength(500)]
        public string drugqnty { get; set; }

        [StringLength(500)]
        public string batchid { get; set; }

        public DateTime? stcheckdate { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(50)]
        public string SysQty { get; set; }
    }
}
