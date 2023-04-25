namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ward_discharge
    {
        [Key]
        public long WDID { get; set; }

        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        public DateTime? dateadmit { get; set; }

        public DateTime? datedischarge { get; set; }

        [StringLength(500)]
        public string diagnosis { get; set; }

        [StringLength(500)]
        public string presentingcomp { get; set; }

        [StringLength(500)]
        public string hispcomp { get; set; }

        [StringLength(500)]
        public string pastmedhis { get; set; }

        [StringLength(500)]
        public string pastsurghis { get; set; }

        [StringLength(500)]
        public string alergies { get; set; }

        [StringLength(500)]
        public string manageinhosp { get; set; }

        [StringLength(500)]
        public string dischargeins { get; set; }

        [StringLength(500)]
        public string followupins { get; set; }

        [StringLength(50)]
        public string BhtNo { get; set; }

        [StringLength(500)]
        public string ConsultantName { get; set; }

        [StringLength(500)]
        public string SickCategory { get; set; }

        [StringLength(100000)]
        public string InvestigationSummary { get; set; }
    }
}
