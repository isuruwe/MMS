namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineCardiovascular")]
    public partial class ExamineCardiovascular
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string PulseRhythmRegular { get; set; }

        [StringLength(50)]
        public string PulseRhythmIregular { get; set; }

        [StringLength(50)]
        public string PulseVolume { get; set; }

        [StringLength(50)]
        public string JVPcmH2O { get; set; }

        [StringLength(50)]
        public string ApexBeat { get; set; }

        [StringLength(50)]
        public string HeartSounds { get; set; }

        [StringLength(50)]
        public string Murmurs { get; set; }

        public string Other { get; set; }

        [StringLength(50)]
        public string CarotidBruit { get; set; }

        [StringLength(50)]
        public string BrachialLeft { get; set; }

        [StringLength(50)]
        public string BrachialRight { get; set; }

        [StringLength(50)]
        public string RadialLeft { get; set; }

        [StringLength(50)]
        public string RadialRight { get; set; }

        [StringLength(50)]
        public string FemoralLeft { get; set; }

        [StringLength(50)]
        public string FemoralRight { get; set; }

        [StringLength(50)]
        public string DorsalisPedisLeft { get; set; }

        [StringLength(50)]
        public string DorsalisPedisRight { get; set; }

        [StringLength(50)]
        public string PosteriorTibialLeft { get; set; }

        [StringLength(50)]
        public string PosteriorTibialRight { get; set; }
    }
}
