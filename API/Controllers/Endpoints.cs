namespace API.Controllers
{
    public static class Endpoints
    {
        public static void AddEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapEndpoints();
            // Grouped endpoint will go here
        }

        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api");

            users.MapGet("", () => "Collections.Users");

            users.MapGet("/{id}", (int id) => "Collections.Users");
                  
            users.MapPost("", () => "Collections.Users.Add(user))");

        }
    }
}
