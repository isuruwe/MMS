namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FamilyDetail
    {
        [Key]
        public int SysID { get; set; }

        [StringLength(20)]
        public string RelativeNo { get; set; }

        [StringLength(20)]
        public string MemberNo { get; set; }

        public int? Rank { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }
    }
}
