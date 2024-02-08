namespace api.DTOs;

public class UserDTO(string username, string token)
{
  public string Username { get; set; } = username;
  public string token { get; set; } = token;
}
