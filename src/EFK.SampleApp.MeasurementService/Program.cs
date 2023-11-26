// Copyright (c) Elias Frank. All rights reserved.

using EFK.SampleApp.Common.Persistance;
using EFK.SampleApp.MeasurementService.Services;
using EFK.SampleApp.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.AddSqlServerDbContext<AppDbContext>("db1");
builder.Services.AddHostedService<AppDbContextInitializer>();
builder.Services.AddHostedService<CreateMeasurement>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
