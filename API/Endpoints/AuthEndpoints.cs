using Application.Services.Interfaces;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var auth = app.MapGroup("/api/auth").WithTags("AUTH");

            auth.MapPost("/Login", async (string email, string password,IAuthService authService) => await authService.LoginAsync(email, password));

            auth.MapPost("/Logout", async (string token, IAuthService authService) => await authService.LogoutAsync(token));
        }
    }
}
