namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_PsnlImageP2
    {
        [Key]
        [StringLength(15)]
        public string SNo { get; set; }

        [StringLength(10)]
        public string SerNo { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProfilePicture { get; set; }
    }
}
