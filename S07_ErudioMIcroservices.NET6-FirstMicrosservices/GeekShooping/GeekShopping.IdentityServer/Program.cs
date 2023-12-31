using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Services;
using Duende.IdentityServer.Services;

namespace GeekShopping.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connection = builder.Configuration["MySqlConnection:MysqlConnectionString"];

            builder.Services.AddDbContext<MySQLContext>(options => options.
             UseMySql(connection,
                new MySqlServerVersion(
                     new Version(8, 0, 32))));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MySQLContext>()
                .AddDefaultTokenProviders();

            var Builder = builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryIdentityResources(
                IdentityConfiguration.IdentityResources)
            .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
            .AddInMemoryClients(IdentityConfiguration.Clients)
            .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IProfileService, ProfileServices>();

            Builder.AddDeveloperSigningCredential();

            var app = builder.Build();

            var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection(); 
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            initializer.Initialize();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}