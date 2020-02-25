using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdeToFood.Controllers
{
    public class RestaurantsMvcController : Controller
    {
        private readonly IRestaurantData restaurantData;
        private readonly IConfiguration config;
        private readonly IHtmlHelper htmlHelper;

        public RestaurantsMvcController(IRestaurantData restaurantData, IConfiguration config, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.config = config;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult Index(string SearchTerm)
        {
            var model = new RestaurantsIndexViewModel();
            model.Message = $"{config["Message"]} {DateTime.Now.ToShortDateString()}!";
            model.Restaurants = restaurantData.GetRestaurants(SearchTerm);
            return View(model);
        }

        public IActionResult Detail(int restaurantId)
        {
            var restaurant = restaurantData.GetRestaurantById(restaurantId);
            if (restaurant == null)
            {
                return View("NotFound");
            }
            
            return View(restaurant);
        }

        [HttpGet]
        public IActionResult Edit(int? restaurantId)
        {
            var model = new RestaurantEditViewModel();
            if (restaurantId.HasValue)
            {
                model.Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
                if (model.Restaurant == null)
                {
                    return View("NotFound");
                }
            }
            else
            {
                model.Restaurant = new Restaurant();
            }

            model.CuisineTypes = htmlHelper.GetEnumSelectList<CuisineType>();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Restaurant.Id == 0)
                {
                    model.Restaurant = restaurantData.Create(model.Restaurant);
                    TempData["Message"] = "The Object is created!";
                }
                else
                {
                    model.Restaurant = restaurantData.Update(model.Restaurant);
                    TempData["Message"] = "The Object is updated!";
                }

                restaurantData.Commit();
                return RedirectToAction("Detail", new { restaurantId = model.Restaurant.Id });
            }

            model.CuisineTypes = htmlHelper.GetEnumSelectList<CuisineType>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int restaurantId)
        {
            var restaurant = restaurantData.GetRestaurantById(restaurantId);
            if (restaurant == null)
            {
                return View("NotFound");
            }

            return View(restaurant);
        }

        [HttpPost]
        public IActionResult Delete(Restaurant model)
        {
            var temp = restaurantData.Delete(model.Id);
            if (temp == null)
            {
                return View("NotFound");
            }

            restaurantData.Commit();
            TempData["Message"] = "The Object is deleted!";
            return RedirectToAction("Index");
        }

    }
}
