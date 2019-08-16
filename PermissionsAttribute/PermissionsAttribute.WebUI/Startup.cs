using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionsAttribute.BLL.Services.AccountService;
using PermissionsAttribute.BLL.Services.ClaimService;
using PermissionsAttribute.BLL.Services.ConfigService;
using PermissionsAttribute.BLL.Services.ProfileService;
using PermissionsAttribute.DAL.Models.Contexts;
using PermissionsAttribute.DAL.Repositories;
using PermissionsAttribute.WebUI.Attributes.PermissionAttribute.Infrastructure;

namespace PermissionsAttribute.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("PermissionsDatabase");
            services
                .AddDbContext<PermissionsDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddHttpContextAccessor();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IConfigService, ConfigService>();

            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/LogIn");
                    options.AccessDeniedPath = new PathString("/Account/Error");
                });

            services.AddTransient<IAuthorizationPolicyProvider, HasPermissionPolicy>();
            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Profile}/{action=GetAllProfiles}");
            });
        }
    }
}
