using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RoleDetailModel
    {
        [Key]
        public Guid RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();

    }
}