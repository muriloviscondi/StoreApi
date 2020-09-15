using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APIStory.Models
{
  public class Product
  {
    public Product()
    {
      Manufacturers = new Collection<Manufacturer>();
      BuyProducts = new Collection<BuyProduct>();
    }
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(150)]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [MaxLength(150)]
    public string ImageUrl { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public bool Active { get; set; }  
    [Required]
    public DateTime RegistrationDate { get; set; }
    [Required]
    public DateTime UpdateDate { get; set; }

    public ICollection<Manufacturer> Manufacturers { get; set; }
    public ICollection<BuyProduct> BuyProducts { get; set; }

  }
}
