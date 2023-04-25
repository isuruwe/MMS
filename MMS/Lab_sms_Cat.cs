namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lab_sms_Cat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Catid { get; set; }

        [StringLength(500)]
        public string CatName { get; set; }
    }
}
