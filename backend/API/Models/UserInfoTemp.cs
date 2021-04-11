using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserInfoTemp
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }

        public DateTime Dob { get; set; }

        public string ImgName { get; set; }

        [ForeignKey("BranchId")]
        public BranchModel Branch { get; set; }
        public int BranchId { get; set; }
        public bool IsAdminAccept { get; set; }

        [ForeignKey("UserId")]
        public UserModel UserModel { get; set; }
        public int UserId { get; set; }

    }
}