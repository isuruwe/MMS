namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailList")]
    public partial class EmailList
    {
        [Key]
        [StringLength(10)]
        public string SvcID { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
