namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clinic_Schedule
    {
        [Key]
        public long event_id { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public long? clinic_id { get; set; }

        public DateTime? event_start { get; set; }

        public DateTime? event_end { get; set; }

        public bool? all_day { get; set; }
    }
}
