using Application.Services.Interfaces;
using Domain.DTO;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api/users").WithTags("Users");

            MapGetUserByEmail(users);
            MapGetAllUsers(users);
            MapGetUserById(users);
            MapAddUser(users);
            MapUpdateUser(users);
            MapDeleteUser(users);

            static void MapGetUserByEmail(IEndpointRouteBuilder users) => users
                .MapGet("/{email}", async (string email, IUserService userService) =>
                {
                    var result = await userService.GetByEmailAsync(email);

                    return result.Success ?
                         Results.Ok(result) :
                         Results.BadRequest(result);
                })
                .WithMetadata(new SwaggerOperationAttribute(
                    summary: "Get user by email",
                    description: "Returns a user by the given email"
                ));

            static void MapGetAllUsers(IEndpointRouteBuilder users) => users
               .MapGet("/", async (IUserService userService) =>
               {
                   var result = await userService.GetAllAsync();

                   return result.Success ?
                        Results.Ok(result) :
                        Results.BadRequest(result);
               })
               .WithMetadata(new SwaggerOperationAttribute(
                   summary: "Get all users",
                   description: "Returns all users"
               ));

            static void MapGetUserById(IEndpointRouteBuilder users) => users
               .MapGet("/{id:guid}", async (Guid id, IUserService userService) =>
               {
                   var result = await userService.GetByIdAsync(id);

                   return result.Success ?
                        Results.Ok(result) :
                        Results.BadRequest(result);
               })
               .WithMetadata(new SwaggerOperationAttribute(
                   summary: "Get user by ID",
                   description: "Returns a user by the given ID"
               ));

            static void MapAddUser(IEndpointRouteBuilder users) => users
               .MapPost("/", async (UserAddDTO user, IUserService userService) =>
               {
                   var result = await userService.AddAsync(user);

                   return result.Success ?
                        Results.Ok(result) :
                        Results.BadRequest(result);
               })
               .WithMetadata(new SwaggerOperationAttribute(
                   summary: "Add a new user",
                   description: "Creates a new user"
               ));

            static void MapUpdateUser(IEndpointRouteBuilder users) => users
               .MapPut("/", async (UserDTO user, IUserService userService) =>
               {
                   var result = await userService.UpdateAsync(user);

                   return result.Success ?
                       Results.Ok(result) :
                       Results.BadRequest(result);
               })
               .WithMetadata(new SwaggerOperationAttribute(
                   summary: "Update an existing user",
                   description: "Updates the details of an existing user"
               ));

            static void MapDeleteUser(IEndpointRouteBuilder users) => users
               .MapDelete("/{id:guid}", async (Guid id, IUserService userService) =>
               {
                   var result = await userService.DeleteAsync(id);

                   return result.Success ?
                       Results.Ok(result) :
                       Results.BadRequest(result);
               })
               .WithMetadata(new SwaggerOperationAttribute(
                   summary: "Delete a user",
                   description: "Deletes a user by the given ID"
               ));

        }
    }
}
