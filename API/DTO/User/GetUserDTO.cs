using System;

namespace API.DTO
{
    public class GetUserDTO
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
}