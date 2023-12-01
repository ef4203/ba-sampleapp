// Copyright (c) Elias Frank. All rights reserved.

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");

var db = builder.AddSqlServerContainer("sql", "PA$9ZWhkP28q")
    .AddDatabase("db1");

var apiService = builder.AddProject<Projects.EFK_SampleApp_ApiService>("apiservice")
    .WithReference(db);

builder.AddProject<Projects.EFK_SampleApp_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);

builder.AddProject<Projects.EFK_SampleApp_MeasurementService>("measurementService")
    .WithReference(db);

builder.Build().Run();
