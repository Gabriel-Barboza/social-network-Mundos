
using BigBrain.SocialNetworkMundos.Domain.Entities;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BigBrain.SocialNetworkMundos.Application.Services
{
    public class TokenService : ITokenService
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        public TokenService(
         IUserRepository userRepository,
         IOptions<JwtSettings> jwtSettings) // Injetar IOptions<JwtSettings>
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value; // Extrai as configurações
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                     new Claim("username", user.Username), 
                     new Claim(JwtRegisteredClaimNames.Email, user.Email) 
                    }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };


            var token = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(token);
            return tokenString;
        }




    }
}

