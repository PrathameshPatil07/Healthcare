using Hospital.BLL.Interfaces;
using Hospital.BLL.Services;
using Hospital.DAL.Context;
using Hospital.DAL.Repository.Implementation;
using Hospital.DAL.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

namespace Healthcare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================================
            // Add Controllers
            // =========================================

            builder.Services.AddControllers();

            // =========================================
            // Swagger Configuration
            // =========================================

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Healthcare API",
                    Version = "v1",
                    Description = "Hospital Management System API"
                });

                // JWT Swagger Support

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter JWT Token"
                    });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            // =========================================
            // Database Context
            // =========================================

            builder.Services.AddSingleton<DapperContext>();

            // =========================================
            // Repository Registration
            // =========================================

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

           // builder.Services.AddScoped<IPatientRepository, PatientRepository>();

           // builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

           // builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // =========================================
            // Service Registration
            // =========================================

            builder.Services.AddScoped<IAuthService, AuthService>();

            //builder.Services.AddScoped<IPatientService, PatientService>();

           // builder.Services.AddScoped<IDoctorService, DoctorService>();

            //builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            // =========================================
            // JWT Authentication
            // =========================================

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        ValidIssuer =
                            builder.Configuration["Jwt:Issuer"],

                        ValidAudience =
                            builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    builder.Configuration["Jwt:Key"]))
                    };
            });

            // =========================================
            // Authorization
            // =========================================

            builder.Services.AddAuthorization();

            // =========================================
            // Build App
            // =========================================

            var app = builder.Build();

            // =========================================
            // Middleware Configuration
            // =========================================

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Healthcare API v1");

                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            // Authentication

            app.UseAuthentication();

            // Authorization

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}