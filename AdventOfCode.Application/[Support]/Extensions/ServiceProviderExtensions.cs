using AdventOfCode.Application.Interfaces;
using AdventOfCode.Application.Operations;
using AdventOfCode.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AdventOfCode.Application;

namespace AdventOfCode.Api;

public static class ServiceProviderExtensions
{
    public static void AddApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationOperations();
        builder.Services.AddApplicationServices();
    }

    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IApplicationOperationManager, ApplicationOperationManager>();
        services.AddTransient<IApplicationOperationResolver, ApplicationOperationResolver>();
    }

    private static void AddApplicationOperations(this IServiceCollection services)
    {
        var applicationOperations = typeof(IApplicationOperationAssemblyMarker)
            .GetAssemblyOfMarker()
            .GetImplementationsOfInterface(typeof(IApplicationOperation));

        foreach (var operation in applicationOperations) 
        {
            services.AddTransient(operation);
        }
    }

}
