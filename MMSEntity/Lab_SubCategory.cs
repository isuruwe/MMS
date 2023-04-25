namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lab_SubCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lab_SubCategory()
        {
            Lab_Report = new HashSet<Lab_Report>();
        }

        [Key]
        [StringLength(10)]
        public string LabTestID { get; set; }

        [StringLength(10)]
        public string CategoryID { get; set; }

        [StringLength(50)]
        public string SubCategoryID { get; set; }

        [StringLength(50)]
        public string SubCategoryName { get; set; }

        [StringLength(50)]
        public string ReferenceRange { get; set; }

        [StringLength(50)]
        public string ReferenceRangeUnit { get; set; }

        public virtual Lab_MainCategory Lab_MainCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lab_Report> Lab_Report { get; set; }
    }
}
