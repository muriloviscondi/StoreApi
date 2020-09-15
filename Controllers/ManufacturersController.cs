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
  public class ManufacturersController : ControllerBase
  {
    private readonly AppDbContext _context;

    public ManufacturersController(AppDbContext context)
    {
      _context = context;
    }

    // GET: Manufacturers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
    {
      return await _context.Manufacturers.ToListAsync();
    }

    // GET: Manufacturers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
    {
      var manufacturer = await _context.Manufacturers.FindAsync(id);

      if (manufacturer == null)
      {
        return NotFound();
      }

      return manufacturer;
    }

    // PUT: Manufacturers/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> PutManufacturer(int id, Manufacturer manufacturer)
    {
      var data = _context.Manufacturers.Find(id);
      if (id != manufacturer.ManufacturerId)
      {
        return BadRequest();
      }

      data.ManufacturerId = id;
      data.CompanyName = manufacturer.CompanyName;
      data.Email = manufacturer.Email;
      data.Phone = manufacturer.Phone;
      data.FederalRegistration = data.FederalRegistration;
      data.StateRegistration = data.StateRegistration;
      data.RegistrationDate = data.RegistrationDate;
      data.UpdateDate = DateTime.Now;
      data.Street = manufacturer.Street;
      data.Number = manufacturer.Number;
      data.Complement = manufacturer.Complement;
      data.Neighborhood = manufacturer.Neighborhood;
      data.City = manufacturer.City;
      data.Uf = manufacturer.Uf;
      data.ProductId = manufacturer.ProductId;

      try
      {
        _context.Manufacturers.Update(data);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ManufacturerExists(id))
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

    // POST: Manufacturers
    [HttpPost("create")]
    public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
    {
      manufacturer.RegistrationDate = DateTime.Now;
      manufacturer.UpdateDate = DateTime.Now;

      _context.Manufacturers.Add(manufacturer);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
    }

    // DELETE: Manufacturers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Manufacturer>> DeleteManufacturer(int id)
    {
      var manufacturer = await _context.Manufacturers.FindAsync(id);
      if (manufacturer == null)
      {
        return NotFound();
      }

      _context.Manufacturers.Remove(manufacturer);
      await _context.SaveChangesAsync();

      return manufacturer;
    }

    private bool ManufacturerExists(int id)
    {
      return _context.Manufacturers.Any(e => e.ManufacturerId == id);
    }
  }
}
