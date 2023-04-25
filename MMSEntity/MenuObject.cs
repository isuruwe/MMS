namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ObjectCode { get; set; }

        [Required]
        [StringLength(10)]
        public string SysID { get; set; }

        public int? ListOrdering { get; set; }

        [Required]
        [StringLength(50)]
        public string ObjID { get; set; }

        [Required]
        [StringLength(30)]
        public string ObjName { get; set; }

        [StringLength(50)]
        public string SubObjID { get; set; }

        [StringLength(30)]
        public string SubObjName { get; set; }

        [StringLength(9)]
        public string SecondSubObjID { get; set; }

        [StringLength(30)]
        public string SecondSubObjName { get; set; }

        [StringLength(50)]
        public string ObjectFileName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(100)]
        public string SmallIcon { get; set; }

        [StringLength(100)]
        public string LargeIcon { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }
    }
}
