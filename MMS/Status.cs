namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status()
        {
            Patient_Detail = new HashSet<Patient_Detail>();
        }

        [Key]
        [Column("Status")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status1 { get; set; }

        [StringLength(50)]
        public string StatusDec { get; set; }

        public int? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Detail> Patient_Detail { get; set; }
    }
}
