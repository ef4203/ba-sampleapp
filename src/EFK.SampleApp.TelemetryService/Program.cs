namespace EFK.SampleApp.TelemetryService;

using EFK.SampleApp.Common.Persistance;
using EFK.SampleApp.TelemetryService.Filter;
using EFK.SampleApp.TelemetryService.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(
            x =>
                x.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

        builder.Services.AddHangfire(
            x =>
                x.UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Db")));

        builder.Services.AddHangfireServer();
        builder.Services.AddLogging();
        builder.Services.AddScoped<MeasurementJob>();

        var app = builder.Build();

        app.MigrateDatabase();
        app.StartJobs();
        app.UseHangfireDashboard(
            "/hangfire",
            new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
            });

        app.MapHangfireDashboard();
        app.Run();
    }

    private static void MigrateDatabase(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }

    private static void StartJobs(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var jobClient = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
        var job = scope.ServiceProvider.GetRequiredService<MeasurementJob>();
        jobClient.AddOrUpdate(nameof(MeasurementJob), () => job.Handle(), Cron.Minutely);
    }
}
