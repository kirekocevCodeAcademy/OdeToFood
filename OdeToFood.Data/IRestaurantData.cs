using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurants(string name = null);
        Restaurant GetRestaurantById(int restaurantId);
        Restaurant Update(Restaurant restaurant);
        int Commit();
        Restaurant Create(Restaurant restaurant);
        Restaurant Delete(int restaurantId);
    }
}