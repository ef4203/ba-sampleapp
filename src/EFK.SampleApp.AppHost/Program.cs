// Copyright (c) Elias Frank. All rights reserved.

using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");

var db = builder.AddSqlServerContainer("sql", "PA$9ZWhkP28q")
    .AddDatabase("db1");

var apiservice = builder.AddProject<EFK_SampleApp_ApiService>("apiservice")
    .WithReference(db);

builder.AddProject<EFK_SampleApp_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiservice);

builder.AddProject<EFK_SampleApp_MeasurementService>("measurementService")
    .WithReference(db);

builder.Build().Run();
