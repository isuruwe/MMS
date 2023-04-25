namespace MMSEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineGeneral")]
    public partial class ExamineGeneral
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string Alert { get; set; }

        [StringLength(50)]
        public string Confused { get; set; }

        [StringLength(50)]
        public string Drowsy { get; set; }

        [StringLength(50)]
        public string Unconscius { get; set; }

        [StringLength(50)]
        public string IsPain { get; set; }

        [StringLength(50)]
        public string PainScore { get; set; }

        [StringLength(50)]
        public string PainLocation { get; set; }

        [StringLength(50)]
        public string BuidLean { get; set; }

        [StringLength(50)]
        public string BuildAverage { get; set; }

        [StringLength(50)]
        public string BuildObese { get; set; }

        [StringLength(50)]
        public string Dyspnoea { get; set; }

        [StringLength(50)]
        public string Cyanosis { get; set; }

        [StringLength(50)]
        public string Pallor { get; set; }

        [StringLength(50)]
        public string Icterus { get; set; }

        [StringLength(50)]
        public string Arcus { get; set; }

        [StringLength(50)]
        public string Xanthomata { get; set; }

        [StringLength(50)]
        public string ExtremitiesWarm { get; set; }

        [StringLength(50)]
        public string ColdandClammy { get; set; }

        [StringLength(50)]
        public string PedalOedema { get; set; }

        [StringLength(50)]
        public string Clubbing { get; set; }

        [StringLength(50)]
        public string SkinRashes { get; set; }

        [StringLength(50)]
        public string SkinUlcers { get; set; }

        [StringLength(50)]
        public string SkinWounds { get; set; }

        [StringLength(50)]
        public string SkinTattoos { get; set; }

        [StringLength(50)]
        public string SkinMoles { get; set; }

        [StringLength(50)]
        public string SkinNavei { get; set; }

        [StringLength(50)]
        public string SkinScars { get; set; }

        [StringLength(50)]
        public string SkinDetails { get; set; }

        [StringLength(50)]
        public string CariousTeech { get; set; }

        [StringLength(50)]
        public string OralUlcers { get; set; }

        [StringLength(500)]
        public string OtherExamination { get; set; }
    }
}
