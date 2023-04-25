using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class getclaimdata
    {
        public int? Service_Type { get; set; }
        public int sv { get; set; }
        public string PID { get; set; }
        public string catagory { get; set; }
        public string diagnosis { get; set; }
        public string Relationship { get; set; }
        public string Surname { get; set; }
        public DateTime? crdate { get; set; }
        public DateTime? evntdate { get; set; }
        public string loc { get; set; }
        public string claimamnt { get; set; }
        public string serviceno { get; set; }
        public string claimname { get; set; }
        public string relasiont { get; set; }
        public string status { get; set; }
        public string moamount { get; set; }
        public string initials { get; set; }
        public string rank { get; set; }
        public string cid { get; set; }
        public string regno { get; set; }
        public int? IsDHSConfirmed { get; set; }
        public string modate { get; set; }
        public string tsimount { get; set; }
        public string tstdate { get; set; }
        public string dhsamount { get; set; }
        public string dhsdate { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual IEnumerable<DateTime?> batchlist { get; set; }
        public virtual IEnumerable<string> ulist { get; set; }
    }
}