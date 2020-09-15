using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APIStory.Models
{
  public class User
  {
    public User()
    {
      BuyProducts = new Collection<BuyProduct>();
    }

    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MaxLength(12)]
    public string Phone { get; set; }
    [Required]
    [MaxLength(11)]
    public string SocialSecurity { get; set; } // CPF
    [MaxLength(10)]
    public string IdentityDocument { get; set; } // RG
    public Genre Genre { get; set; }
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

    public TypeAddressUser TypeAddressUser { get; set; }

    public ICollection<BuyProduct> BuyProducts { get; set; }

  }

  public enum Genre
  {
    H = 1, // Homem
    M = 2, // Mulher
    N = 3, // Não Binário
    C = 4, // Cisgênero
    T = 5, // Transexual
    P = 6 // Prefiro não declarar
  }

  public enum TypeAddressUser
  {
    Trabalho = 1,
    Casa = 2
  }
}
