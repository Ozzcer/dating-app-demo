using System.Security.Cryptography;
using System.Text;
using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
  private readonly DataContext context = context;

  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
  {
    if (await UserExists(registerDTO.Username)) return BadRequest("Username already taken");

    using var hmac = new HMACSHA512();
    var user = new User(registerDTO.Username, hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)), hmac.Key);
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return user;

  }

  [HttpPost("Login")]
  public async Task<ActionResult<User>> Login(LoginDTO loginDTO)
  {
    var user = await context.Users.SingleOrDefaultAsync(user => user.Username.ToLower().Equals(loginDTO.Username.ToLower()));
    if (user is null) return Unauthorized("Invalid user");
    using var hmac = new HMACSHA512(user.PasswordSalt);
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
    if (hash.SequenceEqual(user.PasswordHash)) return user;
    else return Unauthorized("Invalid password");
  }

  private async Task<bool> UserExists(string username)
  {
    return await context.Users.AnyAsync(user => user.Username!.ToLower().Equals(username.ToLower()));
  }

}
