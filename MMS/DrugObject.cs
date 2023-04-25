namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DrugObject
    {
        [Key]
        public int SysID { get; set; }

        public int? ObjID { get; set; }

        [StringLength(50)]
        public string ObjDisplayName { get; set; }

        [StringLength(50)]
        public string ObjFileName { get; set; }

        public int? PerentID { get; set; }

        public int? NodLeval { get; set; }

        public int? IsValidForm { get; set; }

        public int? IconID { get; set; }

        public int? Status { get; set; }
    }
}
