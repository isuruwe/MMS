namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HypRMainCategory")]
    public partial class HypRMainCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HypRMainCategory()
        {
            HypersenseReactions = new HashSet<HypersenseReaction>();
        }

        [Key]
        [StringLength(50)]
        public string HypRMainID { get; set; }

        [StringLength(50)]
        public string HypRMainDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HypersenseReaction> HypersenseReactions { get; set; }
    }
}
