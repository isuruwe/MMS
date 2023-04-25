namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sp_List
    {
        [Key]
        public int spid { get; set; }

        [StringLength(50)]
        public string svcid { get; set; }
    }
}
