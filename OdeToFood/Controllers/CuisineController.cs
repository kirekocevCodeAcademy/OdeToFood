using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Controllers
{
    [Route("api/Restaurants")]
    [ApiController]
    public class CuisineController : ControllerBase
    {
        private readonly IRestaurantData restaurantData;

        public CuisineController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        [HttpGet("cusine")]
        public IActionResult GetAll()
        {
            return Ok(Enum.GetNames(typeof(CuisineType)));
        }

        [HttpGet("{resturantId}/cusine")]
        public IActionResult GetByRestuarntId(int resturantId)
        {
            var resturant = restaurantData.GetRestaurantById(resturantId);
            if(resturant == null)
            {
                return NotFound();
            }
            return Ok(resturant.Cuisine.ToString());
        }
    }
}