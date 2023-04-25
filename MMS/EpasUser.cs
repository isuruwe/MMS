namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EpasUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(50)]
        public string rankid { get; set; }

        [StringLength(500)]
        public string name { get; set; }

        public byte[] password { get; set; }

        [StringLength(20)]
        public string KitIssParentUnit { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string DivisionName { get; set; }
    }
}
