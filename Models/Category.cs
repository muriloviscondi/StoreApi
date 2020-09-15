using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIStory.Models
{
  public class Category
  {
    public Category()
    {
      Products = new Collection<Product>();
    }
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public DateTime RegistrationDate { get; set; }
    [Required]
    public DateTime UpdateDate { get; set; }

    public ICollection<Product> Products { get; set; }
  }
}
