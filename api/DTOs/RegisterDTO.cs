using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RegisterDTO(string username, string password)
{
  [Required]
  public string Username { get; set; } = username;
  [Required]
  public string Password { get; set; } = password;
}
