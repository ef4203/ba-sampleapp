// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Common.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class AppDbContextInitializer(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Manually wait 5 seconds before migrating the database, because SQL Server startup
        // is slow and may take a while until it's possible to fully establish a connection.
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken)
            .ConfigureAwait(false);

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(() => context.Database.MigrateAsync(stoppingToken))
            .ConfigureAwait(false);
    }
}
