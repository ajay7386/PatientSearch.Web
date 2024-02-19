using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PatientSearch.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientSearch.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJwtSecurityToken(string emailId)
        {
            //step 1 Header Info (Add alogorithm)
            var algo = SecurityAlgorithms.HmacSha256;

            //step2 Prepare Paylod(User Info And Claim)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, emailId),
                new Claim("Location",""),
                new Claim("IsAdmin","true"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            //Step 3 signature

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])); //Need to Put in Secure palce like Azure Key vault
            var credentials = new SigningCredentials(securityKey, algo);
            var token = new JwtSecurityToken(
                 issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
