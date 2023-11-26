// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.ApiService.Controller;

using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/measurements")]
public class MeasurementController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public Task<Measurement[]> GetAllAsync()
    {
        return dbContext.Measurements
            .OrderByDescending(x => x.Timestamp)
            .ToArrayAsync();
    }
}
