namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_Establishment
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string STN_CODE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string LocationName { get; set; }

        public int? NoOfDivisions { get; set; }

        public int? NoOfPersonnel { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(10)]
        public string DBName { get; set; }

        [StringLength(255)]
        public string SystemURL { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string SType { get; set; }

        [StringLength(50)]
        public string CmdOffName { get; set; }

        [StringLength(15)]
        public string CmdOffRank { get; set; }

        [StringLength(50)]
        public string CmdControl { get; set; }

        [StringLength(100)]
        public string CommandBy { get; set; }

        [StringLength(50)]
        public string AADOName { get; set; }

        [StringLength(15)]
        public string AADORank { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string CreatedUser { get; set; }

        public int? Priority { get; set; }

        public int? Seniority { get; set; }

        [StringLength(10)]
        public string LocShortName { get; set; }

        [StringLength(1)]
        public string HelpDeskStatus { get; set; }

        [StringLength(10)]
        public string PrintName { get; set; }
    }
}
