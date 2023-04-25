namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CatDiagList")]
    public partial class CatDiagList
    {
        [Key]
        public long CatDiagID { get; set; }

        [StringLength(500)]
        public string dgid { get; set; }

        [StringLength(50)]
        public string PDID { get; set; }

        public virtual CatDaignosi CatDaignosi { get; set; }
    }
}
