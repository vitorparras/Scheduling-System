using Application.Services.Interfaces;

namespace API.Middlewares
{
    public class JwtTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public JwtTokenValidationMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Equals("/api/auth/login", StringComparison.OrdinalIgnoreCase)) 
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                await HandleUnauthorizedAsync(context, "Token is missing");
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            var isValid = await authService.TokenIsValid(token);

            if (!isValid.Data)
            {
                await HandleUnauthorizedAsync(context, "Invalid token");
                return;
            }

            await _next(context);
        }

        private static Task HandleUnauthorizedAsync(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync($"Unauthorized: {message}");
        }
    }
}
