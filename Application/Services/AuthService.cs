using Application.Services.Interfaces;
using Domain.Model;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHistoryRepository _tokenHistoryRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, ITokenHistoryRepository tokenHistoryRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenHistoryRepository = tokenHistoryRepository ?? throw new ArgumentNullException(nameof(tokenHistoryRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !VerifyPassword(user, password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            await SaveTokenToHistoryAsync(user.Id, token);
            return token;
        }

        public async Task LogoutAsync(string token)
        {
            var tokenHistory = await _tokenHistoryRepository.GetTokenHistoryAsync(token) ?? throw new UnauthorizedAccessException("Invalid token.");
            await _tokenHistoryRepository.InvalidateTokenAsync(tokenHistory);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool TokenIsValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken != null && _tokenHistoryRepository.IsTokenValidAsync(token).Result;
            }
            catch
            {
                return false;
            }
        }

        private async Task SaveTokenToHistoryAsync(Guid userId, string token)
        {
            var tokenHistory = new TokenHistory
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                IsValid = true
            };

            await _tokenHistoryRepository.AddTokenHistoryAsync(tokenHistory);
        }

        private bool VerifyPassword(User user, string password)
        {
            // TODO 
            //Implement password verification logic, hash comparison
            return user.Password == password;
        }
    }
}
