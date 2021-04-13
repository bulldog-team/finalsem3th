using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class BranchModel
    {
        [Key]
        public int BranchId { get; set; }

        public string Address { get; set; }
        public string BranchName { get; set; }

        public ICollection<UserModel> Users { get; set; }

    }
}