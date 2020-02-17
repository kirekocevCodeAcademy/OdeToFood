using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class RestaurantDataInMemory : IRestaurantData
    {
        private List<Restaurant> restaurants;
        public RestaurantDataInMemory()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Dominos",
                    Location = "Skopje",
                    Cuisine = CuisineType.Italian
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Jakomo",
                    Location = "Veles",
                    Cuisine = CuisineType.Mexican
                },
                new Restaurant
                {
                    Id=3,
                    Name = "La Pizza",
                    Location = "Radovish",
                    Cuisine = CuisineType.Franch
                },
                new Restaurant
                {
                    Id=4,
                    Name = "La",
                    Location = "Radovish",
                    Cuisine = CuisineType.Franch
                }
            };
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Create(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);

            return restaurant;
        }

        public Restaurant Delete(int restaurantId)
        {
            var tempRes = restaurants.SingleOrDefault(r => r.Id == restaurantId);
            if(tempRes != null)
            {
                restaurants.Remove(tempRes);
            }
            return tempRes;
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return restaurants.SingleOrDefault(r => r.Id == restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurants(string name = null)
        {
            return restaurants.Where(r => string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())).OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var tempRestaurant = restaurants.SingleOrDefault(r => r.Id == restaurant.Id);
            if(tempRestaurant != null)
            {
                tempRestaurant.Name = restaurant.Name;
                tempRestaurant.Location = restaurant.Location;
                tempRestaurant.Cuisine = restaurant.Cuisine;
            }

            return tempRestaurant;
        }
    }
}