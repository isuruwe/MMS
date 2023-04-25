namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CatDaignosi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatDaignosi()
        {
            CatDiagLists = new HashSet<CatDiagList>();
        }

        [Key]
        [StringLength(500)]
        public string dgid { get; set; }

        [StringLength(500)]
        public string dgdetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDiagList> CatDiagLists { get; set; }
    }
}
