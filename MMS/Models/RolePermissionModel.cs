using System.ComponentModel.DataAnnotations;

namespace MMS.Models
{
    public class RolePermissionModel
    {
        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Required(ErrorMessage = "Role name is reqired")]
        [StringLength(35)]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Role permission id is reqired")]
        public int RPermID { get; set; }
        public string SysID { get; set; }
        [Required(ErrorMessage = "Menu item is reqired")]
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
