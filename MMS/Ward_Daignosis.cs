namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_Daignosis
    {
        [Key]
        public int CatDiagID { get; set; }

        public int? dgid { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }
    }
}
