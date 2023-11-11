// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Api.Controllers;

using EFK.SampleApp.Common;
using EFK.SampleApp.Common.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/measurements")]
public class MeasurementController : Controller
{
    private readonly AppDbContext dbContext;

    public MeasurementController(AppDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    public async Task<Measurement[]> GetAllAsync()
    {
        return await this.dbContext.Measurements
            .OrderByDescending(x => x.Timestamp)
            .ToArrayAsync()
            .ConfigureAwait(false);
    }
}
