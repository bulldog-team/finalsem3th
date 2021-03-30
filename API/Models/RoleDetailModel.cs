using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class RoleDetailModel
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public RoleModel RoleModel { get; set; }

    }
}