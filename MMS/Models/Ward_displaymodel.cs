using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS
{
    using System;
    using System.Collections.Generic;

    public partial class Ward_displaymodel
    {
        public long WPID { get; set; }
        public string Pname { get; set; }
        public string Prank { get; set; }
        public string Pwardno { get; set; }
        public string Pbedno { get; set; }
        public string PwardD { get; set; }
        public string Pdiagnosis { get; set; }
        public string Padmitdate { get; set; }
        public string RANK { get; set; }
        public string Ward_ID { get; set; }
        public string PserviceNo { get; set; }
        public int? RelationshipType { get; set; }
    }
}