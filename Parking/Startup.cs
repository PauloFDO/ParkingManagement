using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Parking.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Parking.Entities;
using ApplicationSettings;
using Parking.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using Parking.Code;
using Parking.Managers;

namespace ParkingSpace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddContextElements(services);
            AddViewElements(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            if (ConsumeAppSettingElements.IsAzureActive())
                AddSSO(services);

            AddManagers(services);
        }

        public void AddContextElements(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    ConsumeAppSettingElements.GetConnectionStringFromAppSetting()));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public void AddViewElements(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        public void AddSSO(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                    .AddAzureAD(options => ConsumeAppSettingElements.GetTheAzureOptionsForSSO().Bind(options));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";
                options.TokenValidationParameters = new TokenValidationParameters() { NameClaimType = "name", RoleClaimType = ClaimTypes.Role };
                UserLoginEvents(options, serviceProvider);
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void UserLoginEvents(OpenIdConnectOptions options, ServiceProvider serviceProvider)
        {
            options.Events = new OpenIdConnectEvents()
            {
                OnTicketReceived = context =>
                {
                    StartupCustomServiceActions loginActions = new StartupCustomServiceActions();
                    loginActions.PostLoginActions(context, serviceProvider);
                    return Task.CompletedTask;
                }
            };
        }

        public void AddManagers(IServiceCollection services)
        {
            services.AddScoped<IParkingManagement, ParkingManagementManager>();
            services.AddScoped<IParkingDetailsOnDate, ParkingDetailsOnDateManager>();
            services.AddScoped<IParking, ParkingManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
                endpoints.MapRazorPages();
            });

            CustomActionsOnAppInitialization(app);
        }

        public void CustomActionsOnAppInitialization(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                StartupCustomServiceActions ActionsOnApplicationInitialization = new StartupCustomServiceActions();
                ActionsOnApplicationInitialization.AdministratorUserFromSeeding(scope);
            }
        }
    }
}
