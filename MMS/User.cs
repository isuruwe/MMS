namespace MMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserPermissions = new HashSet<UserPermission>();
            UserRoles = new HashSet<UserRole>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

       
        [StringLength(5)]
        public string RoleID { get; set; }

       
        [StringLength(20)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public byte[] Pass { get; set; }

        public int? Status { get; set; }

        [StringLength(15)]
        public string Salutation { get; set; }

        [StringLength(50)]
        public string FName { get; set; }

        [StringLength(50)]
        public string LName { get; set; }

        [StringLength(20)]
        public string ServiceNo { get; set; }

        [StringLength(50)]
        public string Rank { get; set; }

        [StringLength(50)]
        public string Trade { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        [StringLength(10)]
        public string CompetentTrade { get; set; }

        [StringLength(3)]
        public string LocationID { get; set; }

        [StringLength(5)]
        public string DivisionID { get; set; }

        [StringLength(15)]
        public string NIC { get; set; }

        [StringLength(22)]
        public string ContactNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public int? MaritalStatus { get; set; }

        public DateTime? LastVisit { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUser { get; set; }

        public Guid? msrepl_tran_version { get; set; }

        [StringLength(15)]
        public string Directorate { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
