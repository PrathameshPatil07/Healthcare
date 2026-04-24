
using FluentValidation;
using Healthcare.BLL;
using Healthcare.BLL.Service;
using Healthcare.DAL.Data;
using Healthcare.DAL.Repository.Implementation;
using Healthcare.DAL.Repository.Interface;

namespace Healthcare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.


            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            builder.Services.AddSingleton<DapperContext, DapperContext>();

            builder.Services.AddValidatorsFromAssemblyContaining<PatientValidator>();

            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddSingleton<DapperContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
