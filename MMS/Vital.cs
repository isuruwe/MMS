namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vital
    {
        [Key]
        [StringLength(50)]
        public string VID { get; set; }

       
        [StringLength(50)]
        public string PDID { get; set; }

        public int VTID { get; set; }

        
        [StringLength(30)]
        public string VitalValues { get; set; }

       
        [StringLength(3)]
        public string LocationID { get; set; }

        public DateTime Reading_Time { get; set; }

        [StringLength(10)]
        public string LocID { get; set; }

        public int Status { get; set; }

       
        [StringLength(10)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(15)]
        public string CreatedMachine { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(15)]
        public string ModifiedMachine { get; set; }

        public virtual Patient_Detail Patient_Detail { get; set; }

        public virtual Vital_Type Vital_Type { get; set; }
    }
}
