using API.Middlewares;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Mappings;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
            builder.ConfigureSwagger();

            builder.ConfigureAuthentication();
            builder.Services.AddAuthorization();

            return builder;
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtTokenValidationMiddleware>(); 

            app.ImplementMigrations();
        }

        public static void ImplementMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SchedulerContext>();
            dbContext.Database.Migrate();
        }

        public static WebApplicationBuilder ConfigureRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString("SqlServer");
            builder.Services.AddDbContext<SchedulerContext>(options => options
                .UseSqlServer(connString)
                .EnableSensitiveDataLogging());

            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            return builder;
        }
    }
}
