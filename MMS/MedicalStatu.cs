namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedicalStatu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ServiceNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Height { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BMI_Value { get; set; }

        public int? MedicalStatus { get; set; }
    }
}
