namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryNutrition")]
    public partial class SurgeryNutrition
    {
        [Key]
        public int NID { get; set; }

        [StringLength(500)]
        public string NutDesc { get; set; }
    }
}
