using Microsoft.AspNetCore.Mvc;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Models;
using System;

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

        [HttpGet("{id}", Name = "GetRestaurant")]
        public IActionResult GetRestaurant(int id)
        {
            var data = restaurantData.GetRestaurantById(id);
            if(data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(RestaurantDto restaurantCreateDto)
        {
            if(restaurantCreateDto == null)
            {
                return BadRequest();
            }

            var restaurant = new Restaurant();
            restaurant.Cuisine = (CuisineType)restaurantCreateDto.Cuisine;
            restaurant.Name = restaurantCreateDto.Name;
            restaurant.Location = restaurantCreateDto.Location;
            restaurantData.Create(restaurant);
            restaurantData.Commit();

            return CreatedAtRoute("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public IActionResult Update(RestaurantDto restaurantDto, int id)
        {
            var restaurant = restaurantData.GetRestaurantById(id);
            restaurant.Cuisine = (CuisineType)restaurantDto.Cuisine;
            restaurant.Name = restaurantDto.Name;
            restaurant.Location = restaurantDto.Location;

            restaurantData.Update(restaurant);
            restaurantData.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var temp = restaurantData.Delete(id);
            if(temp == null)
            {
                return BadRequest();
            }

            restaurantData.Commit();
            return NoContent();
        }
    }
}