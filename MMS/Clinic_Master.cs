namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clinic_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clinic_Master()
        {
            Clinic_Alloc = new HashSet<Clinic_Alloc>();
        }

        [Key]
        [StringLength(50)]
        public string Clinic_ID { get; set; }

        [StringLength(50)]
        public string CurrentNo { get; set; }

        [StringLength(50)]
        public string MaxPatients { get; set; }

        public DateTime? ClinicDate { get; set; }

        [StringLength(500)]
        public string Clinic_Detail { get; set; }

        [StringLength(3)]
        public string LocationID { get; set; }

        [StringLength(10)]
        public string SID { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string ClinicDates { get; set; }

        [StringLength(50)]
        public string ClinicTime { get; set; }

        public int ClinicTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clinic_Alloc> Clinic_Alloc { get; set; }

        public virtual Clinic_Type Clinic_Type { get; set; }

        public virtual Location Location { get; set; }
    }
}
