namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Master
    {
        [Key]
        [StringLength(50)]
        public string SID { get; set; }

        [Required]
        [StringLength(3)]
        public string LocationID { get; set; }

        public int Service_Type { get; set; }

        [StringLength(10)]
        public string ServiceNo { get; set; }

        public int SpecialityID { get; set; }

        public int? Rank_ID { get; set; }

        public int Job_CategoryID { get; set; }

        [Required]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Initials { get; set; }

        public byte[] ProfilePicture { get; set; }

        public int Status { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(15)]
        public string CreatedMachine { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(15)]
        public string ModifiedMachine { get; set; }

        [StringLength(50)]
        public string LOCID { get; set; }

        public int? UserID { get; set; }

        public virtual Clinic_Master Clinic_Master { get; set; }

        public virtual Clinic_Type Clinic_Type { get; set; }

        public virtual Job_Category Job_Category { get; set; }

        public virtual Location Location { get; set; }

        public virtual Service_Type Service_Type1 { get; set; }
    }
}
