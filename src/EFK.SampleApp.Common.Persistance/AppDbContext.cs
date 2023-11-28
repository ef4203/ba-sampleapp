// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Common.Persistance;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Measurement> Measurements { get; set; }
}
