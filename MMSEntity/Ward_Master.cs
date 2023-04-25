namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ward_Master()
        {
            Ward_Alloc = new HashSet<Ward_Alloc>();
            Ward_Alloc1 = new HashSet<Ward_Alloc>();
        }

        [Key]
        [StringLength(50)]
        public string Ward_ID { get; set; }

        [StringLength(50)]
        public string CurrentBedCount { get; set; }

        [StringLength(50)]
        public string MaxPatients { get; set; }

        [StringLength(50)]
        public string Bed_Count { get; set; }

        [StringLength(3)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string Ward_Type { get; set; }

        public virtual Location Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ward_Alloc> Ward_Alloc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ward_Alloc> Ward_Alloc1 { get; set; }
    }
}
