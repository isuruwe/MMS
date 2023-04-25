namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_MedicalBoardAuthority
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReqID { get; set; }

        [StringLength(30)]
        public string SNo { get; set; }

        [StringLength(50)]
        public string TypeOfBoard { get; set; }

        public DateTime? DateOfAuth { get; set; }

        public int? ReqStatus { get; set; }
    }
}
