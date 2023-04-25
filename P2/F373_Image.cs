namespace P2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class F373_Image
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(35)]
        public string ImageID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageSeqNo { get; set; }

        [StringLength(15)]
        public string SvcID { get; set; }

        [StringLength(20)]
        public string UniformType { get; set; }

        public DateTime? DateTaken { get; set; }

        [Column(TypeName = "image")]
        public byte[] PersonalImage { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? Status { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedMachine { get; set; }

        [StringLength(15)]
        public string ModifiedUser { get; set; }
    }
}
