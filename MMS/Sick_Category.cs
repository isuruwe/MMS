namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sick_Category
    {
        [Key]
        [StringLength(50)]
        public string CatIndex { get; set; }

        
        [StringLength(50)]
        public string PDID { get; set; }

        public int CatID { get; set; }

        [StringLength(500)]
        public string CatPeriod { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(10)]
        public string LocID { get; set; }

        public virtual Patient_Detail Patient_Detail { get; set; }
    }
}
