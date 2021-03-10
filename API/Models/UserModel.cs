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
        public Guid RoleId { get; set; } = Guid.Parse("b90d1426-8014-11eb-9439-0242ac130002");
    }
}