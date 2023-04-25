namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRole
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Required]
        [StringLength(10)]
        public string LocationID { get; set; }

        [Required]
        [StringLength(5)]
        public string DivisionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(12)]
        public string SysID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUser { get; set; }

        public virtual User User { get; set; }
    }
}
