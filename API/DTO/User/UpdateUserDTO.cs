using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTO.User
{
    public class UpdateUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public Boolean Gender { get; set; }
        public string[] Role { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public DateTime DoB { get; set; }

        public string Image { get; set; }

        public BranchModel Branch { get; set; }
        public int BranchId { get; set; }

        public bool IsAdminAccept { get; set; }

        public string Phone { get; set; }
    }
}