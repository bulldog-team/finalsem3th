using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public Boolean Gender { get; set; }
        public ICollection<RoleDetailModel> RoleDetailModels { get; set; }
        public String Address { get; set; }

        public DateTime Dob { get; set; }

        public String Image { get; set; }

        [ForeignKey("BranchId")]
        public BranchModel Branch { get; set; }
        public int BranchId { get; set; }

        public bool IsAdminAccept { get; set; }

        public string Phone { get; set; }
    }
}