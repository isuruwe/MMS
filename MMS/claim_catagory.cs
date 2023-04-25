namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class claim_catagory
    {
        [Key]
        [StringLength(50)]
        public string mc_catid { get; set; }

        [StringLength(500)]
        public string mc_catdetail { get; set; }
    }
}
