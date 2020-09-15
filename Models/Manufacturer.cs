using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APIStory.Models
{
  public class Manufacturer
  {
    public int ManufacturerId { get; set; }

    [Required]
    [MaxLength(50)]
    public string CompanyName { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MaxLength(12)]
    public string Phone { get; set; }
    [Required]
    [MaxLength(14)]
    public string FederalRegistration { get; set; } // CNPJ
    [MaxLength(15)]
    public string StateRegistration { get; set; } // Inscrição Estadual
    [Required]
    public DateTime RegistrationDate { get; set; }
    [Required]
    public DateTime UpdateDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Street { get; set; }
    [Required]
    [MaxLength(8)]
    public string Number { get; set; }
    [MaxLength(50)]
    public string Complement { get; set; }
    [Required]
    [MaxLength(50)]
    public string Neighborhood { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    [Required]
    [MaxLength(2)]
    public string Uf { get; set; }

    public Product Product { get; set; }
    public int ProductId { get; set; }
  }
}
