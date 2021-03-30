using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        public ICollection<RoleDetailModel> RoleDetailModels { get; set; }
    }
}