namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clinic_Alloc
    {
        [Key]
        [StringLength(50)]
        public string Clinic_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string ClinicID { get; set; }

        public DateTime? Date { get; set; }

        public int? Status { get; set; }

        public string Clinic_Diagnosis { get; set; }

        public virtual Clinic_Master Clinic_Master { get; set; }

        public virtual Patient_Detail Patient_Detail { get; set; }
    }
}
