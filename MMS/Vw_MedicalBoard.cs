namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_MedicalBoard
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mbpsID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string SNo { get; set; }

        public DateTime? dateOfBoard { get; set; }

        [StringLength(50)]
        public string current_MedCat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? wefDate { get; set; }

        public string findingRemarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextMB { get; set; }
    }
}
