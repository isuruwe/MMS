namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Job_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job_Category()
        {
            Staff_Master = new HashSet<Staff_Master>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Job_CategoryID { get; set; }

        [Column("Job_Category")]
        [StringLength(50)]
        public string Job_Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff_Master> Staff_Master { get; set; }
    }
}
