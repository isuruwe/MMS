namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryClosure")]
    public partial class SurgeryClosure
    {
        [Key]
        public long CID { get; set; }

        public int? SuturematerialsID { get; set; }

        [StringLength(500)]
        public string Technique { get; set; }

        [StringLength(50)]
        public string Noofpkts { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }
    }
}
