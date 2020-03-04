using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Core.Auth;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : IdentityDbContext<ApplicationUser>
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
