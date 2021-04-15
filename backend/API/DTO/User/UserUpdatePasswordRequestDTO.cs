namespace API.DTO.User
{
    public class UserUpdatePasswordRequestDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}