namespace HRMS
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RANK1 { get; set; }

        [StringLength(15)]
        public string RNK_NAME { get; set; }

        public int? RANK_TYPE { get; set; }

        [StringLength(255)]
        public string SHORT_NAME { get; set; }

        [StringLength(255)]
        public string LONG_NAME { get; set; }

        public int? SENIORITY { get; set; }

        public int? PROMO_DURATION { get; set; }

        public int? TOTAL_SERVICE_FOR_PROMOTION { get; set; }

        public decimal? TOTAL_SERVICE_FOR_PROMOTION_DE_LAC { get; set; }

        public decimal? TOTAL_SERVICE_FOR_PROMOTION_DE_ACPL { get; set; }

        [StringLength(1)]
        public string TEMPORY_SUBSTANTIVE { get; set; }

        public int? RankGroup { get; set; }

        [StringLength(150)]
        public string AppProm { get; set; }

        [StringLength(50)]
        public string AppPromType { get; set; }

        [StringLength(150)]
        public string NextAppProm { get; set; }

        [StringLength(150)]
        public string PromPaid { get; set; }

        public decimal? TOTAL_SERVICE_FOR_PROMOTION_DE { get; set; }
    }
}
