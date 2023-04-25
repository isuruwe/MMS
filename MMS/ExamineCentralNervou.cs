namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExamineCentralNervou
    {
        [Key]
        [StringLength(50)]
        public string PDID { get; set; }

        [StringLength(50)]
        public string ConciousnessAlert { get; set; }

        [StringLength(50)]
        public string ConciousnessConfused { get; set; }

        [StringLength(50)]
        public string ConciousnessDrowsy { get; set; }

        [StringLength(50)]
        public string ConciousnessUnconscious { get; set; }

        [StringLength(50)]
        public string SpeechNormal { get; set; }

        [StringLength(50)]
        public string SpeechAphasia { get; set; }

        [StringLength(50)]
        public string SpeechDysarthria { get; set; }

        [StringLength(50)]
        public string MentalStateRational { get; set; }

        [StringLength(50)]
        public string MentalStateOriented { get; set; }

        [StringLength(50)]
        public string MentalStateConfused { get; set; }

        [StringLength(50)]
        public string CranialNerves { get; set; }

        [StringLength(50)]
        public string MotorSystem { get; set; }

        [StringLength(50)]
        public string SensorySystem { get; set; }

        [StringLength(50)]
        public string Reflexes { get; set; }

        [StringLength(50)]
        public string GCS { get; set; }

        [StringLength(50)]
        public string NotClinicallyIndicated { get; set; }

        public string Other { get; set; }
    }
}
