namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MED_DEFN
    {
        public long MedSubCatID { get; set; }

        public long MedMainCatID { get; set; }

        [Key]
        public long MedCatID { get; set; }

        public string MedDef { get; set; }
    }
}
