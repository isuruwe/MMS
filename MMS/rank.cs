namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rank()
        {
            Patients = new HashSet<Patient>();
        }

        [Key]
        [Column("RANK")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RANK1 { get; set; }

        [StringLength(15)]
        public string RNK_NAME { get; set; }

        [StringLength(255)]
        public string AUD_REF { get; set; }

        [StringLength(255)]
        public string SHORT_NAME { get; set; }

        [StringLength(255)]
        public string LONG_NAME { get; set; }

        public int? SENIORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
