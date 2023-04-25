namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DrugStore
    {
        [Key]
        [StringLength(20)]
        public string StoresID { get; set; }

        [Required]
        [StringLength(20)]
        public string StationID { get; set; }

        [Required]
        [StringLength(20)]
        public string FormationID { get; set; }

        public int StockType { get; set; }

        [Required]
        [StringLength(20)]
        public string StockName { get; set; }

        public int Status { get; set; }
    }
}
