using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APIStory.Models
{
  public class BuyProduct
  {
    public int BuyProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
    [Required]
    public char Code { get; set; }
    [Required]
    public decimal UnitaryValue { get; set; }
    [Required]
    public decimal Total { get; set; }
    [Required]
    public DateTime RegistrationDate { get; set; }
    [Required]
    public DateTime UpdateDate { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public Product Product { get; set; }
    public int ProductId { get; set; }

  }
}
