namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchStock")]
    public partial class BatchStock
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string ItemID { get; set; }

        public DateTime? DateOfExpire { get; set; }

        public decimal? InQty { get; set; }

        public decimal? OutQty { get; set; }

        public decimal? Price { get; set; }

       
        [StringLength(20)]
        public string StationID { get; set; }

     
        [StringLength(20)]
        public string FormationID { get; set; }

       
        [StringLength(20)]
        public string StoresID { get; set; }

        public int Status { get; set; }
    }
}
