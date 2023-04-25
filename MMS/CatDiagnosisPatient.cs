namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CatDiagnosisPatient")]
    public partial class CatDiagnosisPatient
    {
        [Key]
        public long CDPID { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(500)]
        public string CDID { get; set; }
    }
}
