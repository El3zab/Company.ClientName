using Company.ClientName.BLL;
using Company.ClientName.BLL.Interfaces;
using Company.ClientName.DAL.Data.Contexts;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Helpers;
using Company.ClientName.PL.Helpers.InterfacesHelpers;
using Company.ClientName.PL.Mapping;
using Company.ClientName.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.ClientName.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-in MVC Services
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow DI For DepartmentRepository That Object againest IDepartmentRepository As Refrence
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  // Allow DI For EmployeeRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")); // Configuration Represent any Object"Configration" Inside appsettings.json
                //options.UseSqlServer(builder.Configuration["DefaultConection"]); // As Dictionary Get Value To Key [ DefaultConection ]
            }); // Allow DI For CompanyDbContext [Scoped Life Time] (Can Be Converted To Singleton)

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            builder.Services.AddAutoMapper(typeof(DepartmentProfile));

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IMailService, MailServices>();

            // Allow Dependency Injection By: (If Service Not a Package) [Difference In Life Time]
            //builder.Services.AddScoped();     // Create Object Life Time Per Request - UnReachable Object
            //builder.Services.AddTransient();  // Create Object Life Time Per Operation
            //builder.Services.AddSingleton();  // Create Object Life Time Per Application


            //builder.Services.AddScoped<IScopedService, ScopedService>(); // Per Request
            //builder.Services.AddTransient<ITransiantService, TransiantService>(); // Per Operation
            //builder.Services.AddSingleton<ISingletonService, SingletonService>(); // Per Application

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                             .AddEntityFrameworkStores<CompanyDbContext>()
                             .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            // Only Layer That Bulid Due To It Have Main Method [Install EF Tools Here]
        }
    }
}
