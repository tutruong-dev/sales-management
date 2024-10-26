using Microsoft.AspNetCore.Authentication.Cookies;

namespace SV21T1020793.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews()
                .AddMvcOptions(option =>
                {
                    option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(option =>
                            {
                                option.Cookie.Name = "AuthenticationCookie";
                                option.LoginPath = "/Account/Login";
                                option.AccessDeniedPath = "/Account/AccessDenined";
                                option.ExpireTimeSpan = TimeSpan.FromDays(360);
                            });
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(60);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            ApplicationContext.Configure
                (
                    context: app.Services.GetRequiredService<IHttpContextAccessor>(),
                    enviroment: app.Services.GetRequiredService<IWebHostEnvironment>()
                );

            app.Run();
        }
    }
}