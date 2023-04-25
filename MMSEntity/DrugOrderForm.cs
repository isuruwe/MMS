namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugOrderForm")]
    public partial class DrugOrderForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderFormID { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderFormFrom { get; set; }

        [Required]
        [StringLength(10)]
        public string OrderBySvcNo { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderByName { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(20)]
        public string AuthorityBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime AuthorityDate { get; set; }

        public TimeSpan AuthorityTime { get; set; }

        public DateTime Issueddate { get; set; }

        [Required]
        [StringLength(20)]
        public string IssuedBy { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
