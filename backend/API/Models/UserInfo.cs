using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }

        public DateTime Dob { get; set; }

        public string ImageSrc { get; set; }

        public string ImageName { get; set; }

        [ForeignKey("BranchId")]
        public BranchModel Branch { get; set; }
        public int BranchId { get; set; }
        public bool IsAdminAccept { get; set; }

        [ForeignKey("Id")]
        public UserModel UserModel { get; set; }
        public int UserId { get; set; }

    }
}