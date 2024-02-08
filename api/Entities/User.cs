using System.ComponentModel.DataAnnotations;

namespace api.Entities;

public class User(string username, byte[] passwordHash, byte[] passwordSalt)
{
  public int Id { get; set; }
  public string Username { get; set; } = username;
  public byte[] PasswordHash { get; set; } = passwordHash;
  public byte[] PasswordSalt { get; set; } = passwordSalt;
}
