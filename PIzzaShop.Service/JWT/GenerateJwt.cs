using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PizzaShop.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
namespace PIzzaShop.Service;

public class GenerateJwt : IGenerateJwt
{
    
  
    public string GenerateJwtToken(User user,string role)
    {
       var claims = new List<Claim>
            {
            
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

        var jwtKey ="ICZasdklfjhacknHLisdfnLsdhoSfal4k2342s134k2dfcna234023q42e4w4rIFAKn";
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT key is not configured.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5066",
            audience: "http://localhost:5066",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetUserIdFromJwtToken(string token)
    {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                throw new ArgumentException("Invalid JWT token");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                throw new ArgumentException("JWT token does not contain a user ID");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new ArgumentException("Invalid user ID in JWT token");
            }

            return userId;
        
    }
}
