using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
  private readonly DataContext context = context;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    return await context.Users.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await context.Users.FindAsync(id);
    return user != null ? user : NotFound();
  }
}
