using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetsServer
{
    public class Program
    {
        // https://localhost:7279/login?login=super&password=AQAAAAIAAYagAAAAEO%2FsYj4RkmFNwdqOe88%2B1EZEXC6s3BlUOC2kdjT4ZmPxHBMyWUWRF7SKQ8LzhZunIQ%3D%3D
        // Для получения токена суперпользователя
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Аунтентификация",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Токен",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                });
            });

            // Автомаппер
            builder.Services.AddAutoMapper(typeof(Infrastructure.Services.AutoMapper));

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = config["JwtSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                        ValidateIssuerSigningKey = true,
                    };
                }
                );

            var app = builder.Build();

            app.Map("login", (string login, string password) =>
            {
                UserModel? person = new AuthenticationUserService().GetUser(login);

                if (person is null) return Results.Unauthorized();
                var result = new PasswordHasher<UserModel>().VerifyHashedPassword(person, person.Password, password);
                if (result != PasswordVerificationResult.Success) return Results.Unauthorized();

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Login) };
                var jwt = new JwtSecurityToken(
                        issuer: config["JwtSettings:Issuer"],
                        audience: config["JwtSettings:Audience"],
                        claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(12)),
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    access_token = encodedJwt,
                    username = person.Login
                };
                return Results.Json(response);
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Map("/", [Authorize] () => "Не авторизован!");

            app.MapControllers();

            app.Run();
        }
    }
}