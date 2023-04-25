namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lab_sms
    {
        [Key]
        public long smsid { get; set; }

        [StringLength(500)]
        public string massegetext { get; set; }

        [StringLength(50)]
        public string phoneno { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public DateTime? createdtime { get; set; }

        public DateTime? senttime { get; set; }

        [StringLength(50)]
        public string pid { get; set; }
    }
}
