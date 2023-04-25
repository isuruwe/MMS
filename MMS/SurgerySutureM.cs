namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgerySutureM")]
    public partial class SurgerySutureM
    {
        [Key]
        public int SMID { get; set; }

        [StringLength(500)]
        public string Suturematerials { get; set; }
    }
}
