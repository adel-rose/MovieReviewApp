using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Repositories;
using NHibernate;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var conString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(conString, sql => sql.MigrationsAssembly("MovieApp.Infrastructure"));

    options.UseLazyLoadingProxies();

});



builder.Services.AddScoped<IDapperDbConnection, DapperDbConnection>();
builder.Services.AddScoped<IMovieRespository, MovieRepositoryEF>();
builder.Services.AddScoped<IMovieRepositoryContrib, MovieRepositoryContrib>();
builder.Services.AddScoped<IMovieRepositoryDapper, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreRepository, GenreRepositoryEF>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddTransient<IDiscountable, PlantinumPackage>();
builder.Services.AddTransient<IDiscountable, GoldPackage>();

var app = builder.Build();

// -----------------------
// 1?? Run migrations first
// -----------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();

    var retries = 20; // ? increase
    while (true)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Database migrated successfully.");
            break;
        }
        catch (SqlException ex)
        {
            retries--;

            if (retries == 0)
            {
                throw new Exception(
                    "Could not connect to SQL Server after multiple retries.",
                    ex
                );
            }

            Console.WriteLine("SQL Server not ready yet, retrying in 5s...");
            Thread.Sleep(5000);
        }
    }
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();

app.Run();
