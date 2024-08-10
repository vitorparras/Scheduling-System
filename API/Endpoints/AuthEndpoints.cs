namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api");

            users.MapGet("", () => "Collections.Users");

            users.MapGet("/{id}", (int id) => "Collections.Users");
                  
            users.MapPost("", () => "Collections.Users.Add(user))");
        }
    }
}
