using Application.DTOs.Professional;
using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("AppSettings")["SecretKey"];
        }
        public string Generate(ProfessionalDTOOutput professional)
        {
            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes(_secretKey);

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = GenerateClaims(professional),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(ProfessionalDTOOutput professional)
        {
            ClaimsIdentity ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, professional.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, professional.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, professional.Email));
            ci.AddClaim(new Claim("Function", professional.Function));
            ci.AddClaim(new Claim("Type", ((int)professional.Type).ToString()));
            return ci;
        }
    }
}
 