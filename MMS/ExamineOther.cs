namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineOther")]
    public partial class ExamineOther
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        public string Other { get; set; }
    }
}
