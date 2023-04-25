namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModifiedUser")]
    public partial class ModifiedUser
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Username { get; set; }

        [StringLength(500)]
        public string DateTime { get; set; }
    }
}
