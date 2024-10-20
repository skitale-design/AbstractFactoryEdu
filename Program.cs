using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace AbstractFactoryEdu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionStringsSection = builder.Configuration.GetSection("ConnectionStrings");
            var connectionStrings = connectionStringsSection.GetChildren();

            builder.Services.AddControllersWithViews();

            
            builder.Services.AddSingleton<IEnumerable<CustomDbContext>>(serviceProvider =>
            {
                var dataBases = new List<CustomDbContext>();
                var dbContextFactory = new DbContextFactory();

                foreach (var connection in connectionStrings)
                {
                    dataBases.Add(dbContextFactory.CreateDbContext(connection.Value));
                }

                return dataBases;
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

}
