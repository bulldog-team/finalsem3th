namespace API.DTO.User
{
    public class UserUpdatePasswordRequestDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}