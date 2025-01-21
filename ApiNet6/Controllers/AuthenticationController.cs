using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ApiNet6.Models;
using System.Text.Encodings;
using System.Text;

namespace ApiNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;

        public AuthenticationController(IConfiguration configuration)
        {
            this.secretKey = configuration.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] User user)
        {
            if (user.Email == "1" && user.Password == "1")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5), //expire token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokeConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenText = tokenHandler.WriteToken(tokeConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenText });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Invalid Credentials" });
            }
        }
    }
}
