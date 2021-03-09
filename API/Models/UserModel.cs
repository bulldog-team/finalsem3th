using System;

namespace API.Models
{
  public class UserModel
  {
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }
    public string Email { get; set; }

  }
}