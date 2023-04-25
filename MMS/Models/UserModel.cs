using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MMS.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [MaxLength(5)]
        public string RoleID { get; set; }

        [Required(ErrorMessage = "Please insert user name.", AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please insert password.", AllowEmptyStrings = false)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Please insert confirm password.", AllowEmptyStrings = false)]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConPass { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid status")]
        public Nullable<int> Status { get; set; }

        [MaxLength(15)]
        public string Salutation { get; set; }

        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(50)]
        public string ServiceNo { get; set; }

        [MaxLength(50)]
        public string RANK1 { get; set; }

        [MaxLength(50)]
        public string ClinicTypeID { get; set; }

        [MaxLength(50)]
        public string spid { get; set; }

        [StringLength(50)]
        public string Trade { get; set; }

        [MaxLength(50)]
        public string Designation { get; set; }

        [MaxLength(10)]
        public string CompetentTrade { get; set; }

        [MaxLength(3)]
        public string LocationID { get; set; }

        [MaxLength(3)]
        public string DivisionID { get; set; }

        [MaxLength(15)]
        public string NIC { get; set; }

        [MaxLength(22)]
        public string ContactNo { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Address1 { get; set; }

        [MaxLength(100)]
        public string Address2 { get; set; }

        [MaxLength(100)]
        public string Address3 { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid Datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DOB { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid martial status")]
        public Nullable<int> MaritalStatus { get; set; }


        public Nullable<System.DateTime> LastVisit { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUser { get; set; }

        public Guid? msrepl_tran_version { get; set; }

        [StringLength(15)]
        public string Directorate { get; set; }


        [MaxLength(10)]
        public string Workphone { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserProfile> UserPermissions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}