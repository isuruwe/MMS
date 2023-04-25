using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Models
{
    public class UserProfileModel
    {
        public int UserProfID { get; set; }
        public int UserID { get; set; }
        public string LocationID { get; set; }
        public string DivisionID { get; set; }
        public string RoleID { get; set; }
        public string SysID { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public bool SHO { get; set; }
        public bool NEW { get; set; }
        public bool EDT { get; set; }
        public bool DEL { get; set; }
        public bool PRN { get; set; }
        public bool SER { get; set; }
        public bool CER { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }

    }
}