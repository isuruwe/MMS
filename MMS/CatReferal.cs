namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CatReferal
    {
        [Key]
        public long reffid { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        public string ReffNote { get; set; }

        public string PlanofMgt { get; set; }

        public string DrugHistory { get; set; }
    }
}
