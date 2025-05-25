using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskTracking.Application.Auth;
using TaskTracking.Domain.Entities;

namespace TaskTracking.Infrastructure.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = _configuration;
        }

        public string GenerateToken(User user)
        {
            var settings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(settings["Secret"]);
            var expiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(settings["ExpiryMinutes"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                  issuer: settings["Issuer"],
                  audience: settings["Audience"],
                  claims: claims,
                  expires: expiry,
                  signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
