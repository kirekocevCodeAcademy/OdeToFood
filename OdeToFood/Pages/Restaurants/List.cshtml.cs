using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        
        public string Message { get; set; }

        [TempData]
        public string TempMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }

        public void OnGet()
        {
            Message = $"{config["Message"]} {DateTime.Now.ToShortDateString()}!";
            Restaurants = restaurantData.GetRestaurants(SearchTerm);
        }
    }
}