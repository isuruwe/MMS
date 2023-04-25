using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public partial class userfeedbacks
    {
        public long fid { get; set; }
        public String userid { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
    }
}