namespace P2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rank
    {
        [Key]
        [Column("RANK")]
        [StringLength(10)]
        public string RANK1 { get; set; }

        [StringLength(15)]
        public string RNK_NAME { get; set; }

        public int? RANK_TYPE { get; set; }

        [StringLength(255)]
        public string AUD_REF { get; set; }

        [StringLength(255)]
        public string SHORT_NAME { get; set; }

        [StringLength(255)]
        public string LONG_NAME { get; set; }

        public int? SENIORITY { get; set; }

        public int? PROMO_DURATION { get; set; }

        public int? PROMO_DURATION_MED { get; set; }

        public int? MAX_IN_THE_RANK { get; set; }

        [StringLength(50)]
        public string FILE_REF { get; set; }

        [StringLength(10)]
        public string SUBSTANTIVETEMP { get; set; }

        [StringLength(50)]
        public string PROMNAME { get; set; }

        [StringLength(50)]
        public string SVCCONFM_NAME { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string Vacancy { get; set; }

        [StringLength(50)]
        public string VacancyCode { get; set; }

        [StringLength(100)]
        public string RnkNameSln { get; set; }

        [StringLength(100)]
        public string RnkNameTamil { get; set; }

        public int? RankID { get; set; }
    }
}
