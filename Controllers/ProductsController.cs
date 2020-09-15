using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIStory.Context;
using APIStory.Models;
using Microsoft.AspNetCore.Cors;

namespace APIStory.Controllers
{
  [Route("[Controller]")]
  [ApiController]
  [EnableCors("EnableAll")]
  public class ProductsController : ControllerBase
  {
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
      _context = context;
    }

    // GET: Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      return await _context.Products.ToListAsync();
    }

    // GET: Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var product = await _context.Products.FindAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return product;
    }

    // PUT: Products/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
      var data = _context.Products.Find(id);

      if (id != product.ProductId)
      {
        return BadRequest();
      }

      data.ProductId = id;
      data.Name = product.Name;
      data.Description = product.Description;
      data.Price = product.Price;
      data.ImageUrl = product.ImageUrl;
      data.Stock = product.Stock;
      data.Active = product.Active;
      data.RegistrationDate = data.RegistrationDate;
      data.UpdateDate = DateTime.Now;

      try
      {
        _context.Products.Update(data);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
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

    // POST: Products
    [HttpPost("create")]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
    }

    // DELETE: Products/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return product;
    }

    private bool ProductExists(int id)
    {
      return _context.Products.Any(e => e.ProductId == id);
    }
  }
}
