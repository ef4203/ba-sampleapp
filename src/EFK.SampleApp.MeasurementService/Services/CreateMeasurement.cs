// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.MeasurementService.Services;

using System.Diagnostics;
using System.Security.Cryptography;
using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistence;

public sealed partial class CreateMeasurement(
    IServiceProvider serviceProvider,
    ILogger<CreateMeasurement> logger)
    : IHostedService, IAsyncDisposable
{
    private readonly ILogger logger = logger;

    private Timer? timer;

    public async ValueTask DisposeAsync()
    {
        if (this.timer != null)
        {
            // Dispose of the timer asynchronously when the service is stopped.
            await this.timer.DisposeAsync()
                .ConfigureAwait(false);

            this.timer = null;
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.LogTimerStart();

        // Initialize the timer if it's null, and set it to execute DoWork method periodically.
        // The first parameter is the method to execute, and the second and third parameters
        // define the delay before the first execution and the interval between subsequent executions.
        this.timer ??= new Timer(this.DoWork, null, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.LogTimerStop();

        // Change the timer to stop (infinite delay) when the service is stopped.
        this.timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        using var activity = new Activity(nameof(CreateMeasurement));
        Activity.Current ??= activity;
        activity.Start();

        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var measurement = new Measurement
        {
            Timestamp = DateTime.UtcNow,
            Value = RandomNumberGenerator.GetInt32(1, 100) + (1f / RandomNumberGenerator.GetInt32(1, 10)),
        };

        this.LogAddingMeasurement(measurement.Value);
        dbContext.Measurements.Add(measurement);
        dbContext.SaveChanges();

        activity.Stop();
    }

    [LoggerMessage(LogLevel.Information, $"Registering timer {nameof(CreateMeasurement)}")]
    partial void LogTimerStart();

    [LoggerMessage(LogLevel.Information, $"Stopping timer {nameof(CreateMeasurement)}")]
    partial void LogTimerStop();

    [LoggerMessage(LogLevel.Information, "Adding measurement with value '{Value}'.")]
    partial void LogAddingMeasurement(double value);
}
