namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineRespiratory")]
    public partial class ExamineRespiratory
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string SpO2_FiO2 { get; set; }

        [StringLength(50)]
        public string PFR_LMin { get; set; }

        [StringLength(50)]
        public string MediastinalShiftTrachea { get; set; }

        [StringLength(50)]
        public string MediastinalShiftApex { get; set; }

        [StringLength(50)]
        public string ChestMovement { get; set; }

        [StringLength(50)]
        public string PercussionNote { get; set; }

        [StringLength(50)]
        public string Auscultation { get; set; }

        public byte[] Anterior { get; set; }

        public byte[] Posterior { get; set; }
    }
}
