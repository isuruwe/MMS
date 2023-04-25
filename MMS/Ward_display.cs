namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ward_display
    {
        [Key]
        public long WPID { get; set; }

        [StringLength(500)]
        public string Pname { get; set; }

        [StringLength(500)]
        public string Prank { get; set; }

        [StringLength(500)]
        public string Pwardno { get; set; }

        [StringLength(500)]
        public string Pbedno { get; set; }

        [StringLength(500)]
        public string PwardD { get; set; }

        public string Pdiagnosis { get; set; }

        [StringLength(500)]
        public string Padmitdate { get; set; }

        [StringLength(500)]
        public string PserviceNo { get; set; }

        [StringLength(500)]
        public string PdischargDate { get; set; }

        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string PStatus { get; set; }
    }
}
