namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [StringLength(10)]
        public string LocationID { get; set; }

        [StringLength(10)]
        public string DivisionID { get; set; }

        [Required]
        [StringLength(5)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(12)]
        public string SysID { get; set; }

        public int ObjectCode { get; set; }

        public int? SHO { get; set; }

        public int? NEW { get; set; }

        public int? EDT { get; set; }

        public int? DEL { get; set; }

        public int? PRN { get; set; }

        public int? SER { get; set; }

        public int? CER { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUser { get; set; }

        public virtual User User { get; set; }
    }
}
