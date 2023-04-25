namespace P2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_1250
    {
        [StringLength(10)]
        public string ActiveNo { get; set; }

        [StringLength(10)]
        public string ServiceNo { get; set; }

        [StringLength(50)]
        public string CadetNo { get; set; }

        public int? Rank { get; set; }

        public string Initials { get; set; }

        public string Surname { get; set; }

        public int? Branch_Trade { get; set; }

        public int? BloodGroup { get; set; }

        public int? ColourOfHair { get; set; }

        public int? ColourOfEyes { get; set; }

        [StringLength(500)]
        public string BirthMark { get; set; }

        [StringLength(15)]
        public string NIC_NO { get; set; }

        [StringLength(11)]
        public string DateOfBirth { get; set; }

        public decimal? Height_Feet { get; set; }

        public decimal? Height_Inch { get; set; }

        public int? Expr1 { get; set; }

        [StringLength(30)]
        public string ExpireDate { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [StringLength(100)]
        public string OtherName { get; set; }

        [StringLength(15)]
        public string F1250_No { get; set; }

        [StringLength(30)]
        public string DateOfEnlist { get; set; }

        public int? Service_Type { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TRD_NAME { get; set; }

        [StringLength(15)]
        public string RNK_NAME { get; set; }

        [StringLength(30)]
        public string SHORT_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string SNo { get; set; }
    }
}
