namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImagePath")]
    public partial class ImagePath
    {
        public int id { get; set; }

        [StringLength(50)]
        public string locid { get; set; }

        [StringLength(1000)]
        public string filepath { get; set; }
    }
}
