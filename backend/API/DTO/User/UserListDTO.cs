namespace API.DTO.User
{
    public class UserListDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public bool IsAdminAccept { get; set; }
    }
}