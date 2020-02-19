using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class RestaurantDataSql : IRestaurantData
    {
        private readonly OdeToFoodDbContext odeToFoodDbContext;

        public RestaurantDataSql(OdeToFoodDbContext odeToFoodDbContext)
        {
            this.odeToFoodDbContext = odeToFoodDbContext;
        }

        public int Commit()
        {
            return odeToFoodDbContext.SaveChanges();
        }

        public int Count()
        {
            return odeToFoodDbContext.Restaurants.Count();
        }

        public Restaurant Create(Restaurant restaurant)
        {
            odeToFoodDbContext.Restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Delete(int restaurantId)
        {
            var tempRestaurant = odeToFoodDbContext.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
            if (tempRestaurant != null)
            {
                odeToFoodDbContext.Restaurants.Remove(tempRestaurant);
            }
            return tempRestaurant;
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return odeToFoodDbContext.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurants(string name = null)
        {
            var param = !string.IsNullOrEmpty(name) ? $"{name}%" : name;
            return odeToFoodDbContext.Restaurants.Where(r => string.IsNullOrEmpty(name) || EF.Functions.Like(r.Name, param)).ToList();
        }

        public Restaurant Update(Restaurant restaurant)
        {
            odeToFoodDbContext.Entry(restaurant).State = EntityState.Modified;
            return restaurant;
        }
    }
}
