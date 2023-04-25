namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugStockMaster")]
    public partial class DrugStockMaster
    {
        [Key]
        [StringLength(50)]
        public string ItemIndex { get; set; }

        [StringLength(15)]
        public string ItemID { get; set; }

        [StringLength(500)]
        public string BatchID { get; set; }

        public DateTime? ExpireDate { get; set; }

        public DateTime? MFD { get; set; }

        [StringLength(50)]
        public string LOC { get; set; }

        [StringLength(20)]
        public string StoreID { get; set; }

        [StringLength(500)]
        public string DrugQuantity { get; set; }
    }
}
