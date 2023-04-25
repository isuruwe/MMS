using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class getnursedata
    {
        
            public string Relationship { get; set; }
        public int sv { get; set; }
        public string sno { get; set; }
        public string svt { get; set; }
        public string Initials { get; set; }
        public string Surname { get; set; }
        public string RNK_NAME { get; set; }
        public string PID { get; set; }
        public string SxDetail { get; set; }
        public string Category { get; set; }
        public string Service_Type { get; set; }
        public DateTime? dob { get; set; }
        public DateTime? enl { get; set; }
        public String Height { get; set; }
        public String Weight { get; set; }
        public String BMI_Value { get; set; }
        public String dentalStatus { get; set; }
        public String retiredStatus { get; set; }
    }
}