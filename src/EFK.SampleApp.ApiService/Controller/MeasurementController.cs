// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.ApiService.Controller;

using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/measurements")]
public partial class MeasurementsController(
    AppDbContext dbContext,
    ILogger<MeasurementsController> logger) : ControllerBase
{
    private readonly ILogger<MeasurementsController> logger = logger;

    [HttpGet]
    public Task<Measurement[]> GetAllAsync()
    {
        this.LogGetAll();

        return dbContext.Measurements
            .OrderByDescending(x => x.Timestamp)
            .ToArrayAsync();
    }

    [LoggerMessage(LogLevel.Information, $"{nameof(MeasurementsController)} - {nameof(GetAllAsync)}")]
    private partial void LogGetAll();
}
