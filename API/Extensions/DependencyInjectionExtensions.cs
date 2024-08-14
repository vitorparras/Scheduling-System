using Application.Services;
using Application.Services.Interfaces;
using Domain.Mappings;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repository.Interface;

namespace API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            builder.Services
                  .AddEndpointsApiExplorer()
                  .AddSwaggerGen();

            builder.ConfigureContext();
            builder.ConfigureRepositories();
            builder.ConfigureAutoMapper();
            builder.ConfigureServices();

            return builder;
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI();
            }

            app.UseHttpsRedirection();
        }


        public static WebApplicationBuilder ConfigureRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITokenHistoryRepository, TokenHistoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DxContext>();

            return builder;
        }
        
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            return builder;
        }
    }
}
