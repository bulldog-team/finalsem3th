using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace API.DTO.User
{
    public class UserUpdateInfoDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }

        public DateTime Dob { get; set; }

        public string phone { get; set; }
        public int branchId { get; set; }

        public string ImgName { get; set; }
        public string ImgScr { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}