namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurgeryNProcedure")]
    public partial class SurgeryNProcedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nprid { get; set; }

        public string nprDescription { get; set; }

        public string nprlDescription { get; set; }
    }
}
