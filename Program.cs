using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SolutionSelling.Areas.Identity.Data;
using SolutionSelling.Areas.Items.Data;
using SolutionSelling.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddDbContext<ItemsDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDefaultIdentity<SolutionSellingUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<CartService>();

        builder.Services.AddDistributedMemoryCache();

        // cookies to store session data
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromDays(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            SeedItems.Initialize(services);

            SeedPurchases.Initialize(services);
        }

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

        // cookie
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Items}/{action=Index}/{id?}");

        app.MapRazorPages();



        app.Run();
    }
}


