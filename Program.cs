using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Task_5.Generators;
using Task_5.Packages;
using Task_5.Services;

namespace PersonFaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<FakePersonsGeneratorService>((service) =>
            {
                /// See the available regions at the link https://github.com/bchavez/Bogus/

                RegionsPackage package = new RegionsPackage();
                package.AddRegion("sk", "Slovakia");
                package.AddRegion("en_US", "United States");
                package.AddRegion("fr", "France");
                package.AddRegion("pl", "Poland");
                package.AddRegion("es", "Spain");
                package.AddRegion("ro", "Romain");
                package.AddRegion("sv", "Sweden");

                PersonGenerator personGenerator = new PersonGenerator(package);
                PersonErrorGenerator personErrorGenerator = new PersonErrorGenerator();

                return new FakePersonsGeneratorService(personGenerator, personErrorGenerator);
            });

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                var files = Directory.GetFiles("./wwwroot/libraryExports/");

                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if(fileInfo.CreationTime > DateTime.Now.AddMinutes(10))
                    {
                        fileInfo.Delete();
                    }
                }

                await next.Invoke();
            });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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

