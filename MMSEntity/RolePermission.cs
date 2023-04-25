namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolePermission")]
    public partial class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RPermID { get; set; }

        [Required]
        [StringLength(5)]
        public string RoleID { get; set; }

        [Required]
        public string SysID { get; set; }

        public int? MenuID { get; set; }

        [Required]
        [StringLength(10)]
        public string ObjectCode { get; set; }

        public int? SHO { get; set; }

        public int? NEW { get; set; }

        public int? EDT { get; set; }

        public int? DEL { get; set; }

        public int? PRN { get; set; }

        public int? SER { get; set; }

        public int? CER { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUser { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public virtual Role Role { get; set; }
    }
}
