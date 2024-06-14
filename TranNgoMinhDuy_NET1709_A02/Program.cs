using BusinessObject;
using Microsoft.AspNetCore.Identity;

namespace TranNgoMinhDuy_NET1709_A02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //add dbContext
            builder.Services.AddDbContext<FunewsManagementDbContext>();


            //Add services for authen
            builder.Services.AddIdentity<SystemAccount, IdentityRole<short>>()
                .AddEntityFrameworkStores<FunewsManagementDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });
            // Add services to the container.
            builder.Services.AddRazorPages(options =>
                {
                    options.Conventions.AddPageRoute("/NewsArticles/Index", "");
                });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}