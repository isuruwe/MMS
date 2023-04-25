using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class lablist
    {
        public string TestSID { get; set; }
        public string CategoryID { get; set; }
        public string pdid { get; set; }
        public string testsid { get; set; }
        public string CategoryName { get; set; }
        public string Issued { get; set; }
        public DateTime? RequestedTime { get; set; }
        public int? TubeCategory { get; set; }
    }
}