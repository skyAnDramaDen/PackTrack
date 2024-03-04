using AbcParcel.Common.Contract;
using AbcParcel.Data;
using AbcParcel.Extensions;
using AbcParcel.Services.UserServices;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbcParcel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddIdentityCore<Applicationuser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddSignInManager<SignInManager<Applicationuser>>();

                builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

                var mappingConfig = new MapperConfiguration(mappingConfig =>
                {
                    mappingConfig.AddProfile(new MappingConfiguration());
                });
                var mapper = mappingConfig.CreateMapper();
                builder.Services.AddSingleton(mapper);
                builder.Services.RegisterApplicationService<UserService>();
                builder.Services.AddScoped<UserManager<Applicationuser>>();
                builder.Services.AddSingleton<ISystemClock, SystemClock>();
                builder.Services.AddSwaggerGen();
                builder.Services.SwaggerExtension();
                builder.Services.AddRazorPages();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    //app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                app.SwaggerDocumentation();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseSwagger();
                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


