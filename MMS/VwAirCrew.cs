namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VwAirCrew")]
    public partial class VwAirCrew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int acmRID { get; set; }

        [StringLength(50)]
        public string MES { get; set; }

        [StringLength(30)]
        public string SvcNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? WEF { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextDate { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
