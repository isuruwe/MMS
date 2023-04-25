namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient_Detail()
        {
            Clinic_Alloc = new HashSet<Clinic_Alloc>();
            Drug_Prescription = new HashSet<Drug_Prescription>();
            Lab_Report = new HashSet<Lab_Report>();
            Sick_Category = new HashSet<Sick_Category>();
            Vitals = new HashSet<Vital>();
            Ward_Alloc = new HashSet<Ward_Alloc>();
        }

        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [Required]
        [StringLength(50)]
        public string PID { get; set; }

        [Required]
        [StringLength(500)]
        public string Present_Complain { get; set; }

        [StringLength(500)]
        public string History_PresentComplain { get; set; }

        [StringLength(500)]
        public string Other_Complain { get; set; }

        [StringLength(500)]
        public string History_OtherComplain { get; set; }

        [StringLength(500)]
        public string Special_Sickness { get; set; }

        [StringLength(500)]
        public string Hypersensivity { get; set; }

        [StringLength(500)]
        public string Examination { get; set; }

        [StringLength(500)]
        public string OPD_Diagnosis { get; set; }

        public int Status { get; set; }

        [Required]
        [StringLength(10)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(15)]
        public string CreatedMachine { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(15)]
        public string ModifiedMachine { get; set; }

        [Required]
        [StringLength(50)]
        public string OPDID { get; set; }

        [StringLength(10)]
        public string DocSID { get; set; }

        [StringLength(500)]
        public string Treatment { get; set; }

        [StringLength(500)]
        public string DaignosisID { get; set; }

        public virtual CatDaignosi CatDaignosi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clinic_Alloc> Clinic_Alloc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Drug_Prescription> Drug_Prescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lab_Report> Lab_Report { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Status Status1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sick_Category> Sick_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vital> Vitals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ward_Alloc> Ward_Alloc { get; set; }
    }
}
