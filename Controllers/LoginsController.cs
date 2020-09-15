using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIStory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Cors;

namespace APIStory.Controllers
{
  [Route("[Controller]")]
  [ApiController]
  [EnableCors("EnableAll")]
  public class LoginsController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> GetUser()
    {
      return "UserController :: Acessado em : " + DateTime.Now.ToLongDateString();
    }

    // POST: Users
    [HttpPost("register")]
    public async Task<ActionResult<User>> PostUser([FromBody] Login login)
    {
      var newUser = new IdentityUser
      {
        UserName = login.Email,
        Email = login.Email,
        EmailConfirmed = true
      };

      var result = await _userManager.CreateAsync(newUser, login.Password);

      if (!result.Succeeded)
      {
        return BadRequest(result.Errors);
      }
      await _signInManager.SignInAsync(newUser, false);

      return Ok(GenerateToken(login));
    }

    // POST: Login
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] Login userInfo)
    {
      var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

      if (result.Succeeded)
      {
        return Ok(GenerateToken(userInfo));
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Login Inválido...");
        return BadRequest(ModelState);
      }
    }

    private UserToken GenerateToken(Login userInfo)
    {
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        new Claim("meuPet", "pipoca"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var expiration = DateTime.UtcNow.AddHours(1);
      JwtSecurityToken token = new JwtSecurityToken(
         issuer: null,
         audience: null,
         claims: claims,
         expires: expiration,
         signingCredentials: creds);

      return new UserToken()
      {
        Token = new JwtSecurityTokenHandler().WriteToken(token),
        Expiration = expiration
      };
    }
  }
}

