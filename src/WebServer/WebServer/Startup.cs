using Castle.MicroKernel.Registration;
using Castle.Windsor;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Models.Containers;
using WebServer.Services;

namespace WebServer
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var container = new WindsorContainer();
            container.Register(Component.For<ConsoleLoggerInterceptor>().LifeStyle.Singleton.Named("consoleLogger"));

            services.AddTransient<IOrdersContainer, InDbOrdersContainer>();
            services.AddTransient<IServiceInfoContainer, InDbServicesContainer>();
            services.AddTransient<IUsersContainer, InDbUsersContainer>();
            services.AddTransient<IRolesContainer, InDbRolesContainer>();
            services.AddTransient<IUsersOperations, UserOperations>();
            services.AddTransient<IServicesOperations, ServiceOperations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    "HomeServices",
                    "{controller=Home}/{action=Services}");

                routes.MapRoute(
                    "HomeRegistration",
                    "{controller=Home}/{action=Registration}/{who?}");

                routes.MapRoute(
                    "HomeAuthorization",
                    "{controller=Home}/{action=Authorization}");

   
                routes.MapRoute(
                    "ServicesService",
                    "{controller=Services}/{action=Service}/{id?}");

                routes.MapRoute(
                    "ChangeService",
                    "{controller=Services}/{action=Change}/{id?}");


                routes.MapRoute(
                    "AdminUsers",
                    "{controller=Admin}/{action=Users}");

                routes.MapRoute(
                    "AdminOrders",
                    "{controller=Admin}/{action=Orders}/{type?}");

                routes.MapRoute(
                    "AdminServices",
                    "{controller=Admin}/{action=Services}");


                routes.MapRoute(
                    "ManagerOrders",
                    "{controller=Manager}/{action=Orders}/{type?}");

                routes.MapRoute(
                    "ManagerServices",
                    "{controller=Manager}/{action=Services}");
            });
        }
    }
}