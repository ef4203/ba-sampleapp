// Copyright (c) Elias Frank. All rights reserved.

using EFK.SampleApp.ServiceDefaults;
using EFK.SampleApp.Web;
using EFK.SampleApp.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<MeasurementApiClient>(
    client => client.BaseAddress = new Uri("http://apiservice"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
