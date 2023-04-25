namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelationshipType")]
    public partial class RelationshipType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RTypeID { get; set; }

        [StringLength(500)]
        public string Relationship { get; set; }

        public int? RStatus { get; set; }
    }
}
