namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USER_TYPE
    {
        [Key]
        [StringLength(500)]
        public string User_Type_ID { get; set; }

        [Column("User_Type")]
        [StringLength(500)]
        public string User_Type1 { get; set; }
    }
}
