using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Entities.Models;
using Store.Models.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Controllers
{
    
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly StoreContext _context;

        public UsersController(StoreContext context)
        {
            _context = context;
        }

        // POST api/users/register
        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            try
            {
               
                if (_context.Users.Any(u => u.Username == userDto.Username))
                {
                    return Conflict("Username already exists");
                }

                
                var user = new User
                {
                    Username = userDto.Username,
                    Password = userDto.Password
                };

                
                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok("User registered successfully");
            }
            catch
            {
                return StatusCode(500, "An error occurred while registering the user");
            }
        }

        // POST api/users/login
        [HttpPost("login")]
        public IActionResult Login(UserDto userDto)
        {
            try
            {
                
                var user = _context.Users.SingleOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                
                var token = GenerateJwtToken(user);

                return Ok(new { Token = token });
            }
            catch
            {
                return StatusCode(500, "An error occurred while logging in");
            }
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("mysecretKey12345!#@"); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                 Subject = new ClaimsIdentity(new[]
                {
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                 }),
                
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                 Issuer = "http://localhost:5228", 
                 Audience = "http://localhost:5228",
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString= tokenHandler.WriteToken(token);
            return tokenString;
            
        }


       
    }
}


