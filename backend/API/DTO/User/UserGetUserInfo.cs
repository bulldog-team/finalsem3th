using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.DTO.User
{
    public class UserGetUserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public GenderType Gender { get; set; }
        public string Address { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }
        public int BranchId { get; set; }

        public string ImgName { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }

        public bool IsAdminAccept { get; set; }
    }
}