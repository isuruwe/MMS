namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DrugResever")]
    public partial class DrugResever
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReseverType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string SvcNo { get; set; }

        [StringLength(20)]
        public string ReseveReferance { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
