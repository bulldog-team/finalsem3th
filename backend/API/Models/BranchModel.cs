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

        public String Address { get; set; }

        public ICollection<UserModel> Users { get; set; }

    }
}