namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicalCategory")]
    public partial class MedicalCategory
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string SNo { get; set; }

        [Column("MedicalCategory")]
        [StringLength(50)]
        public string MedicalCategory1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceType { get; set; }
    }
}
