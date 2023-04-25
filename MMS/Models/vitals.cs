using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class vitals
    {
        public string VID { get; set; }
        public string PDID { get; set; }
        public int VTID { get; set; }
        public string VitalValues { get; set; }
        public string LocationID { get; set; }
        public System.DateTime Reading_Time { get; set; }
        public string LocID { get; set; }
        public int Status { get; set; }
       
        public string  VitalType { get; set; }
        public string wdID { get; set; }

        public string description { get; set; }

        public string date { get; set; }

        public string investigated { get; set; }
        public string nurseRemarks { get; set; }
        public string PlanId { get; set; }
    }
}