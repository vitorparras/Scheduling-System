using Application.Services.Interfaces;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var auth = app.MapGroup("/api/auth").WithTags("AUTH");

            auth.MapPost("/Login", async (string email, string password, HttpContext context, IAuthService authService) =>
            {
                var clientIp = context.Connection.RemoteIpAddress?.ToString();
                return await authService.LoginAsync(email, password, clientIp);
            });

            auth.MapPost("/Logout", async (string token, IAuthService authService) =>
            {
                return await authService.LogoutAsync(token);
            });
        }
    }
}
