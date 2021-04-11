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
        public ICollection<RoleDetailModel> RoleDetailModels { get; set; }

        [ForeignKey("Id")]
        public int Id { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}