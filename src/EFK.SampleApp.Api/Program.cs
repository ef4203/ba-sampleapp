namespace EFK.SampleApp.Api;

using EFK.SampleApp.Common.Persistance;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(
            x =>
                x.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

        var app = builder.Build();

        app.MapControllers();
        app.UseCors(
            x =>
            {
                x.AllowAnyOrigin();
            });

        app.Run();
    }
}
