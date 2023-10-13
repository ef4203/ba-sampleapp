namespace EFK.SampleApp.TelemetryService.Jobs;

using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;

public class MeasurementJob
{
    private readonly AppDbContext dbContext;

    private readonly ILogger<MeasurementJob> logger;

    public MeasurementJob(AppDbContext dbContext, ILogger<MeasurementJob> logger)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle()
    {
        this.logger.LogInformation("Adding new measurement...");
        var measurement = new Measurement
        {
            Timestamp = DateTime.UtcNow,
            Value = new Random().NextDouble(),
        };

        this.dbContext.Measurements.Add(measurement);
        await this.dbContext.SaveChangesAsync();
    }
}
