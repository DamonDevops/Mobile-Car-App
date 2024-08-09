
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MobileCarApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            //var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlist.db");
            //var conn = new SqliteConnection($"Data Source={dbPath}");
            var conn = new SqliteConnection($"Data Source=C:\\Develop\\GitlabRepositories\\ClientTest\\Mobile Car App\\MobileCarApp\\MobileCarApp.API\\carlist.db");
            builder.Services.AddDbContext<CarListDbContext>(opt => opt.UseSqlite(conn));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());
            app.MapGet("/cars/{id}", async (CarListDbContext db, int id) => 
                await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
            );
            app.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
            {
                var record = await db.Cars.FindAsync(id);
                if (record is null) return Results.NotFound();

                car.Make = record.Make;
                car.Model = record.Model;
                car.Vin = record.Vin;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/cars/{id}", async (CarListDbContext db, int id) =>
            {
                var record = await db.Cars.FindAsync(id);
                if (record is null) return Results.NotFound();

                db.Cars.Remove(record);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapPost("/cars", async (CarListDbContext db, Car car) =>
            {
                await db.AddAsync(car);
                await db.SaveChangesAsync();

                return Results.Ok(car);
            });
            app.UseAuthorization();

            app.Run();
        }
    }
}
