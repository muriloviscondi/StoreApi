using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIStory.Context;
using APIStory.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace APIStory.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [Route("[Controller]")]
  [ApiController]
  [EnableCors("EnableAll")]
  public class CategoriesController : ControllerBase
  {
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
      _context = context;
    }

    // GET: Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
      return await _context.Categories.Include(x => x.Products).ToListAsync();
    }

    // GET: Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      return category;
    }

    // PUT: Categories/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
      var data = _context.Categories.Find(id);

      if (id != category.CategoryId)
      {
        return BadRequest();
      }

      data.CategoryId = id;
      data.Name = category.Name;
      data.RegistrationDate = data.RegistrationDate;
      data.UpdateDate = DateTime.Now;

      _context.Entry(category).State = EntityState.Modified;

      try
      {
        _context.Categories.Update(data);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CategoryExists(id))
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

    // POST: Categories
    [HttpPost("create")]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
      category.RegistrationDate = DateTime.Now;
      category.UpdateDate = DateTime.Now;

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
    }

    // DELETE: Categories/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Category>> DeleteCategory(int id)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }

      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();

      return category;
    }

    private bool CategoryExists(int id)
    {
      return _context.Categories.Any(e => e.CategoryId == id);
    }
  }
}
