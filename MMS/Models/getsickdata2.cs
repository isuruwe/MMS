using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class getsickdata2
    {
        public string PDID { get; set; }
        public string svcid { get; set; }
        public string ismarried { get; set; }
        public string isliveout { get; set; }
        public string age { get; set; }
        public string service { get; set; }
        public string isduty { get; set; }
        public string islow { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public string LocationID { get; set; }
        public string OPDID { get; set; }
        public string rank { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string cat { get; set; }
        public string catdays { get; set; }
        public string stat { get; set; }
        public string IsMedical { get; set; }
        public int? svt { get; set; }
        public int? rt { get; set; }
    }
}