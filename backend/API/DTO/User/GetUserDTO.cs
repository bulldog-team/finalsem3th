using System;

namespace API.DTO
{
    public class GetUserDTO
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public string[] Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = null;
    }
}