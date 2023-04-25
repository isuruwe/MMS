namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vw_Formation
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string LocationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string DivisionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string DivisionType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(60)]
        public string DivisionName { get; set; }

        [StringLength(80)]
        public string OfficeCommand { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoOfPersonnel { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedUser { get; set; }

        [StringLength(50)]
        public string RptDivisionName { get; set; }

        [StringLength(50)]
        public string Commanding { get; set; }
    }
}
