namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HypersenseReaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReactID { get; set; }

        [StringLength(50)]
        public string RCategory { get; set; }

        [StringLength(50)]
        public string RSubcategory { get; set; }

        public virtual HypRMainCategory HypRMainCategory { get; set; }
    }
}
