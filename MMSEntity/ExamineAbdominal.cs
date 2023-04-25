namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineAbdominal")]
    public partial class ExamineAbdominal
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string Distension { get; set; }

        [StringLength(50)]
        public string FreeFluid { get; set; }

        [StringLength(50)]
        public string Liver { get; set; }

        [StringLength(50)]
        public string Livertxt { get; set; }

        [StringLength(50)]
        public string Spleen { get; set; }

        [StringLength(50)]
        public string Spleentxt { get; set; }

        [StringLength(50)]
        public string PulsatileLumps { get; set; }

        [StringLength(50)]
        public string OtherLumps { get; set; }

        [StringLength(50)]
        public string OtherLumpstxt { get; set; }

        [StringLength(50)]
        public string BowelSoundsAbsent { get; set; }

        [StringLength(50)]
        public string BowelSoundsSluggish { get; set; }

        [StringLength(50)]
        public string BowelSoundsNormal { get; set; }

        [StringLength(50)]
        public string BowelSoundsExaggerated { get; set; }

        public byte[] AbdominalDrawing { get; set; }

        [StringLength(50)]
        public string Other { get; set; }
    }
}
