// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Common.Persistence;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Measurement> Measurements { get; set; }
}
