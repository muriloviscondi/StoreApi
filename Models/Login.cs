using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIStory.Models
{
  public class Login
  {
    [Required]
    [MaxLength(20)]
    public string Email { get; set; }
    [Required]
    [MaxLength(20)]
    public string Password { get; set; }
    [Required]
    [MaxLength(20)]
    public string ConfirmPassword { get; set; }
  }
}
