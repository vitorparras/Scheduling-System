using Application.Services.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var auth = app.MapGroup("/Auth").WithTags("Auth");

            auth.MapPost("/Login", async ([FromBody] LoginDTO login, IAuthService authService, HttpContext context) =>
            {
                var clientIp = context.Connection.RemoteIpAddress?.ToString();
                var response = await authService.LoginAsync(login, clientIp);
                return response.Success
                            ? Results.Json(response)
                            : Results.BadRequest(response);
            });

            auth.MapPost("/Logout", async (HttpContext context, IAuthService authService) =>
            {
                var token = context.Request.Headers.Authorization.ToString();
                var response = await authService.LogoutAsync(token);
                return response.Success
                            ? Results.Json(response)
                            : Results.BadRequest(response);
            });
        }
    }
}
