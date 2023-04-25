namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HypMainCategory")]
    public partial class HypMainCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HypMainCategory()
        {
            Hypersensivities = new HashSet<Hypersensivity>();
            HypersensivityTypes = new HashSet<HypersensivityType>();
        }

        [Key]
        [StringLength(50)]
        public string HypersenceMainCatID { get; set; }

        [StringLength(500)]
        public string HypersenceMainCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hypersensivity> Hypersensivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HypersensivityType> HypersensivityTypes { get; set; }
    }
}
