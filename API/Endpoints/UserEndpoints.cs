using Application.Services.Interfaces;
using Domain.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("Users").WithTags("Users");

            users.MapGet("/{email}", async (string email, IUserService userService) =>
            {
                var result = await userService.GetByEmailAsync(email);
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("GetUserByEmail")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Get user by email",
                description: "Returns a user by the given email"
            ));

            users.MapGet("/", async (IUserService userService) =>
            {
                var result = await userService.GetAllAsync();
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("GetAllUsers")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Get all users",
                description: "Returns all users"
            ));

            users.MapGet("/{id:guid}", async (Guid id, IUserService userService) =>
            {
                var result = await userService.GetByIdAsync(id);
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("GetUserById")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Get user by ID",
                description: "Returns a user by the given ID"
            ));

            users.MapPost("/", async (UserAddDTO user, IUserService userService) =>
            {
                var result = await userService.AddAsync(user);
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("AddUser")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Add a new user",
                description: "Creates a new user"
            ));

            users.MapPut("/", async (UserDTO user, IUserService userService) =>
            {
                var result = await userService.UpdateAsync(user);
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("UpdateUser")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Update an existing user",
                description: "Updates the details of an existing user"
            ));

            users.MapDelete("/{id:guid}", async (Guid id, IUserService userService) =>
            {
                var result = await userService.DeleteAsync(id);
                return result.Success
                            ? Results.Json(result)
                            : Results.BadRequest(result);
            })
            .WithName("DeleteUser")
            .WithMetadata(new SwaggerOperationAttribute(
                summary: "Delete a user",
                description: "Deletes a user by the given ID"
            ));
        }
    }
}
