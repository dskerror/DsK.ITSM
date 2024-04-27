using DsK.ITSM.Shared.DTOs;
using DsK.ITSM.Shared.Token;
using DsK.ITSM.API.HttpClients;
using DsK.ITSM.EntityFramework.Models;
using DsK.ITSM.Infrastructure.APIServices;
using DsK.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DsK.ITSM.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<AuthorizarionServerAPIHttpClient>();
        builder.Services.AddHttpClient<AuthorizarionServerAPIHttpClient>("AuthorizarionServerAPI", c =>
        {
            c.BaseAddress = new System.Uri("https://localhost:7045");
        });


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DsKITSMContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("cn"));
        });


        builder.Services.AddScoped<SMTPMailService>();
        builder.Services.AddScoped<RequestsAPIService>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


        builder.Services.Configure<TokenSettingsModel>(builder.Configuration.GetSection("TokenSettings"));

        var IssuerSigningKey = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Key") ?? "";
        if (IssuerSigningKey == "")
        {
            return; //Exit app if IssuerSigningKey is not found
        }

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
                    ValidateIssuer = true,
                    ValidAudience = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });




        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "myOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("myOrigins");
        app.MapControllers();
        app.Run();
    }
}