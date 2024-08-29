using Application.Services.Interfaces;
using Domain.DTO;
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
        private readonly IUserService _userService;
        private readonly ITokenHistoryRepository _tokenHistoryRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            ITokenHistoryRepository tokenHistoryRepository,
            IConfiguration configuration,
            IUserService userService)
        {
            _tokenHistoryRepository = tokenHistoryRepository ?? throw new ArgumentNullException(nameof(tokenHistoryRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<GenericResponse<string>> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                if (!user.Success) return new GenericResponse<string>(user.Erros);

                var passValid = await _userService.VerifyPasswordAsync(user.Data, password);
                if (!passValid.Success) return new GenericResponse<string>(passValid.Erros);

                var token = GenerateJwtToken(user.Data);
                if (!token.Success) return new GenericResponse<string>(token.Erros);

                var tokenHistory = await SaveLoginAsync(user.Data.Id, token.Data);
                if (!tokenHistory.Success) return new GenericResponse<string>(tokenHistory.Erros);

                return token;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user by email", ex);
            }
        }

        public async Task<GenericResponse<string>> LogoutAsync(string token)
        {
            try
            {
                var tokenHistory = await _tokenHistoryRepository.GetTokenHistoryAsync(token) ?? throw new UnauthorizedAccessException("Invalid token.");
                if (tokenHistory is null) return new GenericResponse<string>("Token Not Found", false);

                await _tokenHistoryRepository.InvalidateTokenAsync(tokenHistory);

                return new GenericResponse<string>("User logged out successfully");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to log the user out", ex);
            }
        }

        private GenericResponse<string> GenerateJwtToken(UserDTO user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
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
                return new GenericResponse<string>(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to generate jwt token", ex);
            }
        }

        public async Task<GenericResponse<bool>> TokenIsValid(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var tokenValid = await _tokenHistoryRepository.IsTokenValidAsync(token);

                return new GenericResponse<bool>(validatedToken != null && tokenValid);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to generate jwt token", ex);
            }
        }

        private async Task<GenericResponse<LoginHistory>> SaveLoginAsync(Guid userId, string token)
        {
            try
            {
                var tokenHistory = new LoginHistory
                {
                    UserId = userId,
                    Token = token,
                    IsValid = true,
                };

                var add = await _tokenHistoryRepository.AddAsync(tokenHistory);

                return (add != null) ?
                    new GenericResponse<LoginHistory>(add) :
                    new GenericResponse<LoginHistory>("TokenHistory not saved", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Save TokenToHistory", ex);
            }
        }
    }
}
