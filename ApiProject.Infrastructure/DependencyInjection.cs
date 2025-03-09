using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.PhoneCases;
using ApiProject.Domain.Users;
using ApiProject.Infrastructure.Authentication;
using ApiProject.Infrastructure.Data;
using ApiProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiProject.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Add Jwt provider
        services.AddScoped<ITokenProvider, TokenProvider>();

        // Add Google auth service
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();

        // Add Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPhoneCaseRepository, PhoneCaseRepository>();

        return services;
    }
}
