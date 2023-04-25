namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PersonalDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string SNo { get; set; }

        [StringLength(50)]
        public string ServiceNo { get; set; }

        [StringLength(255)]
        public string Rank { get; set; }

        public int? RankID { get; set; }

        public string Initials { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        [StringLength(50)]
        public string Branch { get; set; }

        public DateTime? DateOfEnlist { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? ExpireDate { get; set; }

        [StringLength(6)]
        public string BloodGroup { get; set; }

        [StringLength(15)]
        public string F1250No { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string Gender { get; set; }

        [StringLength(10)]
        public string ServiceStatus { get; set; }

        public int? Service_Type { get; set; }
    }
}
