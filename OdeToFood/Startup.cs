using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

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
            //services.AddScoped<IRestaurantData, RestaurantDataInMemory>();
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

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();      
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(HelloWorld);
            app.UseMiddleware<CustomMiddleware>();


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

        private RequestDelegate HelloWorld(RequestDelegate next)
        {
            return async httpContext =>
            {
                if (httpContext.Request.Path.StartsWithSegments("/helloInLine"))
                {
                    await httpContext.Response.WriteAsync("Hello World From InLine!");
                }
                else
                {
                    await next(httpContext);
                }
            };

        }
    }

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRestaurantData restaurantData)
        {            
            if (httpContext.Request.Path.StartsWithSegments("/hello"))
            {
                var res = restaurantData.GetRestaurants().ToList();
                await httpContext.Response.WriteAsync($"Hello World {res.Count}!");
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
