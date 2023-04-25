namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryMaster")]
    public partial class SurgeryMaster
    {
        [Key]
        public long SGID { get; set; }

        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        public DateTime? DateofSurgery { get; set; }

        public DateTime? DateodDischarge { get; set; }

        public DateTime? DateofAdmit { get; set; }

        public int? TheaterTable { get; set; }

        public int? ProcedureName { get; set; }

        public int? AnesthesiaType { get; set; }

        public string Indication { get; set; }

        public string AntibioticP { get; set; }

        [StringLength(50)]
        public string Surgeon { get; set; }

        [StringLength(50)]
        public string AssistedBy { get; set; }

        [StringLength(50)]
        public string Anesthetist { get; set; }

        [StringLength(50)]
        public string SurgeryStart { get; set; }

        [StringLength(50)]
        public string SurgeryEnd { get; set; }

        public bool? Catheter { get; set; }

        public bool? CentralIVline { get; set; }

        public bool? Epidural { get; set; }

        public string Findings { get; set; }

        public string PrcedureDetail { get; set; }

        public string DrainsInserted { get; set; }

        public string Specimens { get; set; }

        public string SpecimensHistology { get; set; }

        public string SpecimensMicrobiology { get; set; }

        public string MonitoringInstruct { get; set; }

        public int? Nutritionid { get; set; }

        public string Nutrition { get; set; }

        public string SpecialIntruct { get; set; }

        [StringLength(50)]
        public string Nurse { get; set; }
    }
}
