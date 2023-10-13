namespace EFK.SampleApp.TelemetryService;

using EFK.SampleApp.Common.Persistance;
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

        AutoMigrate(app);
        StartJobs(app);
        app.Run();
    }

    private static void AutoMigrate(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }

    private static void StartJobs(IHost app)
    {
        var job = app.Services.GetRequiredService<MeasurementJob>();
        Task.Run(
            async () =>
            {
                while (true)
                {
                    await job.Handle();
                    await Task.Delay(TimeSpan.FromSeconds(60));
                }
            });
    }
}
