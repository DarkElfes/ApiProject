global using MediatR;
global using ApiProject.Shared.Abstractions.Result;
using ApiProject.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApiProject.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicaction(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);
        services.AddValidatorsFromAssembly(typeof(Shared.Users.Commands.RegisterUserCommand).Assembly);

        // Add MediatR 
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
