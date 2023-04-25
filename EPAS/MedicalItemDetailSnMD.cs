namespace EPAS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicalItemDetailSnMD")]
    public partial class MedicalItemDetailSnMD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string RequisitionAllocationNo { get; set; }

        [StringLength(50)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string DivisionName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ItemNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string ItemDescription { get; set; }

        [StringLength(10)]
        public string DOQ { get; set; }

        [StringLength(2)]
        public string ItemClass { get; set; }

        public decimal? QtyIssueByGroup { get; set; }

        [StringLength(30)]
        public string IssueDate { get; set; }
    }
}
