using API.Endpoints;

namespace API.Extensions
{
    public static class EndpointsExtensions
    {
        public static void ConfigureEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapAuthEndpoints();

        }
    }
}
