using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class getdrugdata
    {
        public string ItemID { get; set; }
        public string LOC { get; set; }
       
        public string StoreID { get; set; }
        public string DrugQuantity { get; set; }
        public string itemdescription { get; set; }
        public string returnqty { get; set; }
        public string remarks { get; set; }
        public string batchid { get; set; }
    }
}