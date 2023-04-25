namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VwAllPaidClaimGVS")]
    public partial class VwAllPaidClaimGV
    {
        [StringLength(50)]
        public string ServiceNo { get; set; }

        [StringLength(255)]
        public string Rank { get; set; }

        public string Name { get; set; }

        [StringLength(350)]
        public string Description { get; set; }

        [StringLength(255)]
        public string LocationName { get; set; }

        [StringLength(30)]
        public string Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalAmount { get; set; }

        [StringLength(100)]
        public string ChequeNumber { get; set; }

        [StringLength(50)]
        public string BeneficiaryName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegisterId { get; set; }

        [StringLength(50)]
        public string SNo { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public DateTime? DateSorted { get; set; }
    }
}
