namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Patient_Complain
    {
        [Key]
        public int ComplainId { get; set; }

        public int WardId { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string InvestigateBy { get; set; }

        public string Remarks { get; set; }

        [StringLength(50)]
        public string RemarksBy { get; set; }
    }
}
