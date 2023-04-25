namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HypersenseSeverty")]
    public partial class HypersenseSeverty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SevertyID { get; set; }

        [StringLength(50)]
        public string SevertyType { get; set; }
    }
}
