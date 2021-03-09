using System;
using System.Collections;

namespace API.Models
{
  public class RoleDetailModel
  {
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public IList UserId { get; set; }

  }
}