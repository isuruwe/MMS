using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class meddata
    {
      
            public long msid { get; set; }
            public string pdid { get; set; }
            public Nullable<int> msyear { get; set; }
            public string msstation { get; set; }
            public string msage { get; set; }
            public string msheight { get; set; }
            public string msweight { get; set; }
            public string msbmi { get; set; }
            public string msbp { get; set; }
            public string msvision { get; set; }
            public string mshbac { get; set; }
            public string msusugar { get; set; }
            public string msfbs { get; set; }
            public string mstotalc { get; set; }
            public string msexecg { get; set; }
            public string msmes { get; set; }
        public string msreason { get; set; }
        public string msspecs { get; set; }
        public int pftsession { get; set; }
        public Nullable<int> msfitness { get; set; }
            public Nullable<System.DateTime> createddate { get; set; }
            public string createduser { get; set; }
            public string modifieduser { get; set; }
            public Nullable<System.DateTime> modifieddate { get; set; }
            public Nullable<int> status { get; set; }
        public int? dentalst { get; set; }
    }
}