namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            Clinic_Master = new HashSet<Clinic_Master>();
            OPD_Master = new HashSet<OPD_Master>();
            Patients = new HashSet<Patient>();
            Staff_Master = new HashSet<Staff_Master>();
            Ward_Master = new HashSet<Ward_Master>();
        }

        [StringLength(3)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string STN_CODE { get; set; }

        [Required]
        [StringLength(30)]
        public string LocationName { get; set; }

        public int? NoOfDivisions { get; set; }

        public int? NoOfPersonnel { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(30)]
        public string DBName { get; set; }

        [StringLength(255)]
        public string SystemURL { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string SType { get; set; }

        [StringLength(50)]
        public string CmdOffName { get; set; }

        [StringLength(15)]
        public string CmdOffRank { get; set; }

        [StringLength(50)]
        public string AADOName { get; set; }

        [StringLength(15)]
        public string AADORank { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedUser { get; set; }

        public int? Priority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clinic_Master> Clinic_Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPD_Master> OPD_Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff_Master> Staff_Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ward_Master> Ward_Master { get; set; }
    }
}
