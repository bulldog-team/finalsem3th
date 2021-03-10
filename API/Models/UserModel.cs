using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }

        [ForeignKey("RoleId")]
        public RoleDetailModel Role { get; set; }
        public Guid RoleId { get; set; }
    }
}