// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.ApiService.Controller;

using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/measurements")]
public partial class MeasurementController(
    AppDbContext dbContext,
    ILogger<MeasurementController> logger) : ControllerBase
{
    private readonly ILogger<MeasurementController> logger = logger;

    [HttpGet]
    public Task<Measurement[]> GetAllAsync()
    {
        this.LogGetAll();

        return dbContext.Measurements
            .OrderByDescending(x => x.Timestamp)
            .ToArrayAsync();
    }

    [LoggerMessage(LogLevel.Information, $"{nameof(MeasurementController)} - {nameof(GetAllAsync)}")]
    private partial void LogGetAll();
}
