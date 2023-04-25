using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class getsickdata
    {
       
            public string PDID { get; set; }
            public string svcid { get; set; }
            public string ismarried { get; set; }
            public string isliveout { get; set; }
            public Nullable<int> age { get; set; }
            public Nullable<double> service { get; set; }
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
        public int stat { get; set; }
    }
}