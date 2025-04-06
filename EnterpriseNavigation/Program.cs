using EnterpriseNavigation.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseNavigation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ApplicationDbcontext>(  options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("devconnection")) ); 

            var app = builder.Build();

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCekxxWmFZfVtgcV9HZlZURGY/P1ZhSXxWdkZgWH5ZcXJQRmZdVEJ9XUs=");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=dashboard}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
