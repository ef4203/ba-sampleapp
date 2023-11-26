// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.MeasurementService.Services;

using System.Diagnostics;
using System.Security.Cryptography;
using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;

public partial class CreateMeasurement(IServiceProvider serviceProvider, ILogger<CreateMeasurement> logger) : IHostedService, IAsyncDisposable
{
    private readonly ILogger logger = logger;

    private Timer? timer;

    public async ValueTask DisposeAsync()
    {
        if (this.timer is IAsyncDisposable timerInstance)
        {
            await timerInstance.DisposeAsync()
                .ConfigureAwait(false);
        }

        this.timer = null;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.LogTimerStart();
        this.timer = new Timer(this.DoWork, null, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.LogTimerStop();
        this.timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        if (Activity.Current is null)
        {
            var activity = new Activity(nameof(CreateMeasurement))
                .Start();

            Activity.Current = activity;
        }

        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var measurement = new Measurement
        {
            Timestamp = DateTime.UtcNow,
            Value = RandomNumberGenerator.GetInt32(1, 100) + 1f / RandomNumberGenerator.GetInt32(1, 10),
        };

        this.LogAddingMeasurement(measurement.Value);
        dbContext.Measurements.Add(measurement);
        dbContext.SaveChanges();

        Activity.Current.Stop();
    }

    [LoggerMessage(LogLevel.Information, $"Registering timer {nameof(CreateMeasurement)}")]
    private partial void LogTimerStart();

    [LoggerMessage(LogLevel.Information, $"Stopping timer {nameof(CreateMeasurement)}")]
    private partial void LogTimerStop();

    [LoggerMessage(LogLevel.Information, "Adding measurement with value '{Value}'.")]
    private partial void LogAddingMeasurement(double value);
}
