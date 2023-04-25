namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_PsnlImageP3
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string SNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string SerNo { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
