namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Mgt_Plan
    {
        [Key]
        public int PlanId { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string InvestigateBy { get; set; }

        public string Remarks { get; set; }

        [StringLength(50)]
        public string RemarksBy { get; set; }
    }
}
