using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIStory.Context;
using APIStory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace APIStory.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [Route("[Controller]")]
  [ApiController]
  [EnableCors("EnableAll")]
  public class UsersController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
      _context = context;
    }

    // GET: Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      return await _context.Users.Include(x => x.BuyProducts).ToListAsync();
    }

    // GET: Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      return user;
    }

    // PUT: Users/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
      var data = _context.Users.Find(id);
      if (id != user.UserId)
      {
        return BadRequest();
      }

      data.UserId = id;
      data.Name = user.Name;
      data.Email = user.Email;
      data.Phone = user.Phone;
      data.SocialSecurity = data.SocialSecurity;
      data.IdentityDocument = data.IdentityDocument;
      data.Genre = user.Genre;
      data.RegistrationDate = data.RegistrationDate;
      data.UpdateDate = DateTime.Now;
      data.Street = user.Street;
      data.Number = user.Number;
      data.Complement = user.Complement;
      data.Neighborhood = user.Neighborhood;
      data.City = user.City;
      data.Uf = user.Uf;

      try
      {
        _context.Users.Update(data);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: Users
    [HttpPost("create")]
    public async Task<ActionResult<User>> PostUser([FromBody] User user)
    {
      user.RegistrationDate = DateTime.Now;
      user.UpdateDate = DateTime.Now;

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return user;
    }

    private bool UserExists(int id)
    {
      return _context.Users.Any(e => e.UserId == id);
    }
  }
}
