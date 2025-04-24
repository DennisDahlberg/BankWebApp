using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.APIs;
using Services.Interfaces;
using System.Globalization;

namespace BankWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<BankAppDataContext>(options =>
                    options.UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BankAppDataContext>();
                builder.Services.AddRazorPages();

                // Add Mapster to the container
                builder.Services.AddMapster();


                var cultureInfo = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


                //Dependencies
                builder.Services.AddTransient<DataInitializer>();
                builder.Services.AddTransient<ICustomerService, CustomerService>();
                builder.Services.AddTransient<ICountryService, CountryService>();
                builder.Services.AddTransient<IRandomUserService, RandomUserService>();
                builder.Services.AddTransient<IAccountService, AccountService>();
                builder.Services.AddTransient<ITransactionService, TransactionService>();
                builder.Services.AddTransient<IUserService, UserService>();
                builder.Services.AddResponseCaching();
                builder.Services.AddHttpClient();


                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.
                         GetRequiredService<BankAppDataContext>();
                    if (dbContext.Database.IsRelational())
                    {
                        dbContext.Database.Migrate();
                    }
                }

                using (var scope = app.Services.CreateScope())
                {
                    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseResponseCaching();

                app.UseAuthorization();

                app.MapStaticAssets();
                app.MapRazorPages()
                   .WithStaticAssets();

                app.Run();
            }
            catch (Exception ex)
            {
                File.WriteAllText("startup-crash.txt", ex.ToString());
                throw;
            }
        }
    }
}
