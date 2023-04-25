namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HypersensivityType")]
    public partial class HypersensivityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HypersensivityType()
        {
            Hypersensivities = new HashSet<Hypersensivity>();
        }

        [Key]
        [StringLength(50)]
        public string HyperTypeID { get; set; }

        [StringLength(50)]
        public string HyperType { get; set; }

        [StringLength(50)]
        public string HyperSubType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hypersensivity> Hypersensivities { get; set; }

        public virtual HypMainCategory HypMainCategory { get; set; }
    }
}
