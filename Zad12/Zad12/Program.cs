using Microsoft.EntityFrameworkCore;
using Zad12.Models;
using Zad12.Repositories;
using Zad12.Services;

namespace Zad12;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        builder.Services.AddControllers();

        builder.Services.AddDbContext<MyDbContext>(opt =>
        {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        opt.UseSqlServer(connectionString);
        });
        builder.Services.AddScoped<ITripsService, TripsService>();
        builder.Services.AddScoped<ITripsRepository, TripsRepository>();
        builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
        builder.Services.AddScoped<IClientsService, ClientsService>();
        
        
        builder.Services.AddOpenApi();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}