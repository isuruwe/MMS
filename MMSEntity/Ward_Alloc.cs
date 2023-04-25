namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Alloc
    {
        [Key]
        [StringLength(50)]
        public string Ward_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string WardID { get; set; }

        public DateTime? Date { get; set; }

        public int? Status { get; set; }

        public string Ward_Diagnosis { get; set; }

        public virtual Patient_Detail Patient_Detail { get; set; }

        public virtual Ward_Master Ward_Master { get; set; }

        public virtual Ward_Master Ward_Master1 { get; set; }
    }
}
