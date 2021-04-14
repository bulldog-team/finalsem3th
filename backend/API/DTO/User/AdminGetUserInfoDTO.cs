using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.DTO.User
{
    public class AdminGetUserInfoDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public GenderType Gender { get; set; }
        public string Address { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }
        public int BranchId { get; set; }

        public string ImgName { get; set; }

        public bool IsAdminAccept { get; set; }
        public string BranchName { get; set; }
    }
}