using Application.Services.Interfaces;
using Domain.DTO;
using Domain.Model;
using Infrastructure.Repository.Interface;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILoginRepository _loginRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            ILoginRepository tokenHistoryRepository,
            IUserService userService,
            IJwtService jwtService)
        {
            _loginRepository = tokenHistoryRepository ?? throw new ArgumentNullException(nameof(tokenHistoryRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<GenericResponse<string>> LoginAsync(string email, string password, string ip)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                if (!user.Success) return new GenericResponse<string>(user.Erros);

                var passValid = await _userService.VerifyPasswordAsync(user.Data, password);
                if (!passValid.Success) return new GenericResponse<string>(passValid.Erros);

                var token = _jwtService.GenerateJwtToken(user.Data);
                if (!token.Success) return new GenericResponse<string>(token.Erros);

                var tokenHistory = await SaveLoginAsync(user.Data.Id, token.Data, ip);
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
                var loginHistory = await _loginRepository.GetLoginHistoryByTokenAsync(token);
                if (loginHistory == null)
                    return new GenericResponse<string>("Token Not Found", false);

                await _loginRepository.InvalidateTokenAsync(loginHistory);

                return new GenericResponse<string>("User logged out successfully");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to log the user out", ex);
            }
        }

        private async Task<GenericResponse<LoginHistory>> SaveLoginAsync(Guid userId, string token, string ip)
        {
            try
            {
                var tokenHistory = new LoginHistory
                {
                    UserId = userId,
                    Token = token,
                    IsValid = true,
                    IPAddress = ip,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(50)
                };

                var add = await _loginRepository.AddAsync(tokenHistory);

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
