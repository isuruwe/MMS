namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DrugItem
    {
        [Key]
        [StringLength(15)]
        public string ItemID { get; set; }

        [StringLength(50)]
        public string ItemShortDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemDescription { get; set; }

        [StringLength(10)]
        public string UOF { get; set; }

        public int TypeOfForm { get; set; }

        public int DrugGroup { get; set; }

        public int StorageCodition { get; set; }

        public int Status { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
