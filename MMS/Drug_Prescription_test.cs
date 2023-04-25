namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Drug_Prescription_test
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Ps_Index { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string PDID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string ItemNo { get; set; }

        [StringLength(500)]
        public string Dose { get; set; }

        public int? Route { get; set; }

        public int? Method { get; set; }

        [StringLength(500)]
        public string Duration { get; set; }

        [StringLength(50)]
        public string LocID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Issued { get; set; }

        public int? DrugCategory { get; set; }

        public DateTime? Date_Time { get; set; }

        [StringLength(10)]
        public string PrescribeBy { get; set; }

        [StringLength(10)]
        public string GivenBy { get; set; }

        [StringLength(10)]
        public string IssuedLocID { get; set; }

        [StringLength(10)]
        public string RequestedLocID { get; set; }

        [StringLength(50)]
        public string issuedQuantity { get; set; }

        public int? MethodType { get; set; }
    }
}
