using Application.Services.Interfaces;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api/auth");

            users.MapPost("/Login", async (string email, string password,IAuthService authService) => await authService.LoginAsync(email, password));

            users.MapPost("/Logout", async (string token, IAuthService authService) => await authService.LogoutAsync(token));
        }
    }
}
