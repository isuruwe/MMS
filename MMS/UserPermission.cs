namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserPermission")]
    public partial class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserProfID { get; set; }

        public int UserID { get; set; }

       
        public string SysID { get; set; }

        public int MenuID { get; set; }

        [StringLength(50)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string DivisionID { get; set; }

       
        [StringLength(5)]
        public string RoleID { get; set; }

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

        public virtual User User { get; set; }
    }
}
