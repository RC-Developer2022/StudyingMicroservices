using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsservices.Infrastructure.Core.Context;

namespace Microsservices.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(""))
        );

        return services;
    }

    public static async void UseEFCoreCreateDatabaseAsync(this IServiceScope serviceScope) 
    {
        using (var service = serviceScope.ServiceProvider.CreateScope())
        {
            var context = service.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!await context.Database.CanConnectAsync())
            {
                await context.Database.EnsureCreatedAsync();
            }
            await context.Database.MigrateAsync();
        }
    }

}
