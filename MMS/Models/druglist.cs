using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class druglist
    {
        public string issuedQuantity { get; set; }
        public DateTime? Date_Time { get; set; }
        public string itemdescription { get; set; }
        public string MethodDetail { get; set; }
        public string Ps_Index { get; set; }
        public decimal? DrugMethodCount { get; set; }
        public string RouteDetail { get; set; }
        public string Duration { get; set; }
        public int? MethodType { get; set; }
        public string Dose { get; set; }
        public string pdid { get; set; }
        public int? Route { get; set; }
        public int? Method { get; set; }
      
        public string ItemNo { get; set; }

        public string date { get; set; }
        public string Issued { get; set; }
    }
}