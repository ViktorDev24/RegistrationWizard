using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Data;
using RegistrationWizard.Interfaces;
using RegistrationWizard.Interfaces.IRepository;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models;
using RegistrationWizard.Repositories;
using RegistrationWizard.Services;

namespace RegistrationWizard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
            builder.Services.AddScoped<IProvinceService, ProvinceService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var angularAppUrl = builder.Configuration.GetValue<string>("AngularAppUrl") ?? "http://localhost:4200";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins(angularAppUrl) 
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
