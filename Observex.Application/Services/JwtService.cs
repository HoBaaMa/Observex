using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Observex.Application.DTOs.Identity;
using Observex.Application.Interfaces;
using Observex.Core.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Observex.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponseDto GenerateJwtToken(ApplicationUser applicationUser)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()), // Subject (user ID)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique ID

                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issuer at (date & time of token generation)
                new Claim(ClaimTypes.NameIdentifier, applicationUser.DisplayUserName), // Unique name identifier of the user (username) 
                new Claim(ClaimTypes.Name, applicationUser.FullName), // Name of the user 
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"], 
                claims, 
                expires: expiration, 
                signingCredentials: signingCredentials);


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return new AuthenticationResponseDto() { 
                DisplayUserName = applicationUser.DisplayUserName, 
                FullName = applicationUser.FullName,
                ExpirationTime = expiration,
                Token = token
                };
        }

    }
}