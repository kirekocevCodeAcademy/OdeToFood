using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddAuthorization(options =>{options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));});

            services.AddMvc();
            services.AddRazorPages().AddRazorPagesOptions(op =>
            {
                op.Conventions.AuthorizeFolder("/Restaurants", "RequireAdministratorRole");
                op.Conventions.AllowAnonymousToPage("/Restaurants/List");
                op.Conventions.AllowAnonymousToPage("/Restaurants/NotFound");
                op.Conventions.AllowAnonymousToPage("/Restaurants/Detail");
            });
            services.AddDbContextPool<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb")));
            services.AddScoped<IRestaurantData, RestaurantDataSql>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();      
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "restaurant",
                    pattern: "{controller}/{action=Index}/{restaurantId?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
