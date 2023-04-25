namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IssuedHeader")]
    public partial class IssuedHeader
    {
        [Key]
        [StringLength(25)]
        public string IssuedNo { get; set; }

        [StringLength(50)]
        public string IssuedTo { get; set; }

        public DateTime? IssuedDate { get; set; }
    }
}
