namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Patient_Detail = new HashSet<Patient_Detail>();
        }

        [Key]
        [StringLength(50)]
        public string PID { get; set; }

       
        [StringLength(3)]
        public string LocationID { get; set; }

        public int Service_Type { get; set; }

       
        [StringLength(10)]
        public string ServiceNo { get; set; }

        public int? RANK { get; set; }

        public int? RelationshipType { get; set; }

        public string Surname { get; set; }

        [StringLength(50)]
        public string Initials { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Sex { get; set; }

        public int? BGID { get; set; }

        public int? MedCatID { get; set; }

        public int? Status { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(15)]
        public string CreatedMachine { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(15)]
        public string ModifiedMachine { get; set; }

        public int? ChildNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Detail> Patient_Detail { get; set; }

        public virtual rank rank1 { get; set; }
    }
}
