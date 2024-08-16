using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MobileCarApp.API;

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
        builder.Services.AddAuthentication();

        //var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlist.db");
        //var conn = new SqliteConnection($"Data Source={dbPath}");
        var conn = new SqliteConnection($"Data Source=C:\\Develop\\GitlabRepositories\\ClientTest\\Mobile Car App\\MobileCarApp\\MobileCarApp.API\\carlist.db");
        builder.Services.AddDbContext<CarListDbContext>(opt => opt.UseSqlite(conn));

        builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CarListDbContext>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
            };
        });
        builder.Services.AddAuthorization(options => {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes()
            .RequireAuthenticatedUser()
            .Build();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        //GET
        app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());
        app.MapGet("/cars/{id}", async (CarListDbContext db, int id) =>
            await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
        );
        //PUT
        app.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
        {
            var record = await db.Cars.FindAsync(id);
            if (record is null) return Results.NotFound();

            record.Make = car.Make;
            record.Model = car.Model;
            record.Vin = car.Vin;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        //DELETE
        app.MapDelete("/cars/{id}", async (CarListDbContext db, int id) =>
        {
            var record = await db.Cars.FindAsync(id);
            if (record is null) return Results.NotFound();

            db.Cars.Remove(record);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        //POST
        app.MapPost("/cars", async (CarListDbContext db, Car car) =>
        {
            await db.AddAsync(car);
            await db.SaveChangesAsync();

            return Results.Ok(car);
        });
        app.MapPost("/login", async (LoginDTO loginDTO, CarListDbContext db, UserManager<IdentityUser> _userManager) => 
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if(user is null)
            {
                return Results.Unauthorized();
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!isValidPassword)
            {
                return Results.Unauthorized();
            }

            //Generate token
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            var tokenClass = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), //user.Id
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email), //user.Email
                new Claim("email_confirmed", user.Email)
            }.Union(claims)
            .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new SecurityTokenDescriptor
            {
                Issuer = builder.Configuration["JwtSettings:Issuer"],
                Audience = builder.Configuration["JwtSettings:Audience"],
                Subject = new ClaimsIdentity(tokenClass),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationInMinutes"])),
                SigningCredentials = credentials
            };

            //var token = new JsonWebToken(
            //    issuer: builder.Configuration["JwtSettings:Issuer"],
            //    audience: builder.Configuration["JwtSettings:Audience"],
            //    claims: tokenClass,
            //    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationInMinutes"])),
            //    signingCredentials: credentials
            //    );

            var accessToken = new JsonWebTokenHandler().CreateToken(token);

            var response = new AuthResponseDTO
            {
                UserId = user.Id,
                Username = user.UserName,
                Token = accessToken
            };

            return Results.Ok(response);
        }).AllowAnonymous();

        app.UseAuthorization();

        app.Run();
    }
    internal class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    internal class AuthResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}