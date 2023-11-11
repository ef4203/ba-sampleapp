// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.TelemetryService.Jobs;

using System.Security.Cryptography;
using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;

public partial class MeasurementJob
{
    private readonly AppDbContext dbContext;

    private readonly ILogger<MeasurementJob> logger;

    public MeasurementJob(AppDbContext dbContext, ILogger<MeasurementJob> logger)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleAsync()
    {
        this.LogAddingMeasurement();
        var measurement = new Measurement
        {
            Timestamp = DateTime.UtcNow,
            Value = RandomNumberGenerator.GetInt32(1, 100) + (1f / RandomNumberGenerator.GetInt32(1, 10)),
        };

        this.dbContext.Measurements.Add(measurement);
        await this.dbContext.SaveChangesAsync()
            .ConfigureAwait(false);
    }

    [LoggerMessage(LogLevel.Information, "Adding new measurement...")]
    private partial void LogAddingMeasurement();
}
