namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedKeyword
    {
        [Key]
        public long MKID { get; set; }

        public string MKDetail { get; set; }
    }
}
