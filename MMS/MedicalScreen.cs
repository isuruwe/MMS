namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicalScreen")]
    public partial class MedicalScreen
    {
        [Key]
        public long msid { get; set; }

        [StringLength(50)]
        public string pdid { get; set; }

        public int? msyear { get; set; }

        [StringLength(50)]
        public string msstation { get; set; }

        [StringLength(50)]
        public string msage { get; set; }

        [StringLength(50)]
        public string msheight { get; set; }

        [StringLength(50)]
        public string msweight { get; set; }

        [StringLength(50)]
        public string msbmi { get; set; }

        [StringLength(50)]
        public string msbp { get; set; }

        [StringLength(50)]
        public string msvision { get; set; }

        [StringLength(50)]
        public string mshbac { get; set; }

        [StringLength(50)]
        public string msusugar { get; set; }

        [StringLength(50)]
        public string msfbs { get; set; }

        [StringLength(50)]
        public string mstotalc { get; set; }

        [StringLength(50)]
        public string msexecg { get; set; }

        [StringLength(50)]
        public string msmes { get; set; }

        public int? msfitness { get; set; }

        public DateTime? createddate { get; set; }

        [StringLength(10)]
        public string createduser { get; set; }

        [StringLength(10)]
        public string modifieduser { get; set; }

        public DateTime? modifieddate { get; set; }

        public int? status { get; set; }

        [StringLength(100)]
        public string msldl { get; set; }

        [StringLength(100)]
        public string mstrigliceried { get; set; }

        [StringLength(1000)]
        public string msreason { get; set; }

        [StringLength(50)]
        public string msspecs { get; set; }

        public int? pftsession { get; set; }
    }
}
