using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;

namespace OdeToFood.Controllers
{
    [Route("api/Restaurants")]
    [ApiController]
    public class RestaurantApiController : ControllerBase
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantApiController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
               
        [HttpGet]
        public IActionResult GetRestaurantsAll()
        {
            var data = restaurantData.GetRestaurants();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var data = restaurantData.GetRestaurantById(id);
            if(data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}