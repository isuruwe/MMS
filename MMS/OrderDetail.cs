namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

       
        [StringLength(10)]
        public string SeqID { get; set; }

        
        [StringLength(15)]
        public string ItemID { get; set; }

       
        [StringLength(20)]
        public string OrderRefNo { get; set; }

        public int DemandQty { get; set; }

        public int IssuedQty { get; set; }

        public int TransectionID { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
