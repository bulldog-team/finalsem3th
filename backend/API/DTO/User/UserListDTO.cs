namespace API.DTO.User
{
    public class UserListDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public bool IsAdminAccept { get; set; }
    }
}